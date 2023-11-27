using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;
using System.Linq.Expressions;

namespace OrderService.DAL.Orders
{
    public class PostgresEfOrderItemsRepository : IOrderItemsRepository
    {
        readonly ApplicationContext _dbContext;

        public PostgresEfOrderItemsRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(IEnumerable<OrderItem> orderItems)
        {
            await _dbContext.AddRangeAsync(orderItems);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderItem>> GetAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            return await _dbContext.OrderItems.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(IEnumerable<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
                _dbContext.Entry(item).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsycn(IEnumerable<OrderItem> orderItems)
        {
            _dbContext.OrderItems.RemoveRange(orderItems);
            await _dbContext.SaveChangesAsync();
        }
    }
}
