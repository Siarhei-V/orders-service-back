using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;

namespace OrderService.DAL.ApplicationLogs
{
    public class PostgresEfLogsRepository : ILogsRepository
    {
        readonly ApplicationContext _ordersContext;

        public PostgresEfLogsRepository(ApplicationContext ordersContext) => _ordersContext = ordersContext;

        public async Task CreateAsync(Log logModel)
        {
            await _ordersContext.AddAsync(logModel);
            await _ordersContext.SaveChangesAsync();
        }
    }
}
