using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;

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

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _ordersContext.Set<T>().AsNoTracking().Where(predicate).ToList();
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
