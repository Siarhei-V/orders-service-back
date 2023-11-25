using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;

namespace OrderService.DAL.Orders
{
    // TODO: add UoW
    public class PostgresEfOrderItemsRepository : PostgresEfCommonRepository<Order>, IOrderItemsRepository
    {
        public PostgresEfOrderItemsRepository(ApplicationContext ordersContext) : base(ordersContext)
        {
        }

        public async Task CreateAsync(IEnumerable<OrderItem> orderItems)
        {
            await _ordersContext.AddRangeAsync(orderItems);
            await _ordersContext.SaveChangesAsync();
        }
    }
}
