using Microsoft.Extensions.DependencyInjection;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using Polly;

namespace OrderService.BLL.Infrastructure
{
    public class BackgroundDataHandler : IBackgroundDataHandler
    {
        readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundDataHandler(IServiceScopeFactory serviceScopeFactory) => _serviceScopeFactory = serviceScopeFactory;

        public void HandleLog(Exception exception, int attemptsNumber = 9)
        {
            var log = new Log { LogEvent = exception.GetType().Name, Description = exception.PrepareException() };
            Handle(log, attemptsNumber);
        }

        private void Handle(Log logModel, int attemptsNumber)
        {
            var retryPolicy = Policy.Handle<Exception>().WaitAndRetryAsync(attemptsNumber, attemptsNumber =>
            {
                Console.WriteLine(attemptsNumber + " " + DateTime.Now); // TODO: remove after debugging
                return TimeSpan.FromSeconds(Math.Pow(attemptsNumber, 2));
            });

            _ = Task.Run(async () =>
            {
                try
                {
                    await using var scope = _serviceScopeFactory.CreateAsyncScope();
                    var logsRepository = scope.ServiceProvider.GetRequiredService<ILogsRepository>();

                    await retryPolicy.ExecuteAsync(async () => await logsRepository.CreateAsync(logModel));
                }
                catch (Exception ex)
                {
                    await Console.Out.WriteLineAsync(ex.ToString());
                }
            });
        }
    }
}