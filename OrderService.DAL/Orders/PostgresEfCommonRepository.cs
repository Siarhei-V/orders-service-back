using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;
using System.Linq.Expressions;

namespace OrderService.DAL.Orders
{
    public class PostgresEfCommonRepository<T> : IRepository<T> where T : BaseEntity
    {
        private protected readonly ApplicationContext _ordersContext;

        public PostgresEfCommonRepository(ApplicationContext ordersContext) => _ordersContext = ordersContext;

        public async Task<int> CreateAsync(T entity)
        {
            var savingResult = await _ordersContext.Set<T>().AddAsync(entity);
            await _ordersContext.SaveChangesAsync();

            return savingResult.Entity.Id;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _ordersContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _ordersContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _ordersContext.Entry(entity).State = EntityState.Modified;
            await _ordersContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _ordersContext.Set<T>().Remove(entity);
            await _ordersContext.SaveChangesAsync();
        }
    }
}
