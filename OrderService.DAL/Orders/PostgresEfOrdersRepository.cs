using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;
using System.Linq.Expressions;

namespace OrderService.DAL.Orders
{
    public class PostgresEfOrdersRepository : IOrdersRepository
    {
        readonly ApplicationContext _dbContext;

        public PostgresEfOrdersRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _dbContext.Orders.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IEnumerable<Order> GetOrdersWithProvider(Expression<Func<Order, bool>> predicate)
        {
            IQueryable<Order> result = _dbContext.Orders.Include(o => o.Provider);
            return result.Where(predicate).ToList();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
        }

        public async Task DeleteAsycn(Order order)
        {
            _dbContext.Orders.Remove(order);
        }
    }
}
