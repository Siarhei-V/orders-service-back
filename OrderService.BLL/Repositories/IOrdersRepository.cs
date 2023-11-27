using OrderService.BLL.Models;
using System.Linq.Expressions;

namespace OrderService.BLL.Repositories
{
    public interface IOrdersRepository
    {
        Task<int> CreateAsync(Order order);
        Task DeleteAsycn(Order order);
        Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate);
        IEnumerable<Order> GetOrdersWithProvider(Func<Order, bool> predicate);
        Task UpdateAsync(Order order);
    }
}
