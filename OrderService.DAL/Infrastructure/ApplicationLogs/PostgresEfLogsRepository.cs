using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.DAL.Infrastructure.ApplicationLogs
{
    public class PostgresEfLogsRepository : ILogsRepository
    {
        readonly ApplicationContext _ordersContext;

        public PostgresEfLogsRepository(ApplicationContext ordersContext) => _ordersContext = ordersContext;

        public async Task CreateAsync(Log logModel)
        {
            var res1 = await _ordersContext.AddAsync(logModel);
            await _ordersContext.SaveChangesAsync();
        }
    }
}
