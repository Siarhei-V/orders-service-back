using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;

namespace OrderService.DAL.Orders
{
    public class PostgresEfOrdersRepository : PostgresEfCommonRepository<Order>, IOrdersRepository
    {
        public PostgresEfOrdersRepository(ApplicationContext ordersContext) : base(ordersContext)
        {
        }

        public IEnumerable<Order> GetOrdersWithProvider(Func<Order, bool> predicate)
        {
            IQueryable<Order> result = _ordersContext.Orders.Include(o => o.Provider);
            return result.Where(predicate).ToList();
        }
    }
}
