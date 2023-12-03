using OrderService.BLL.Models;
using System.Linq.Expressions;

namespace OrderService.BLL.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<int> CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAsync();
        Task UpdateAsync(T entity);
    }
}