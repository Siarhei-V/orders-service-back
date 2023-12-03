using OrderService.BLL.Models;
using System.Linq.Expressions;

namespace OrderService.BLL.Repositories
{
    public interface IOrderItemsRepository
    {
        Task CreateAsync(IEnumerable<OrderItem> orderItems);
        Task DeleteAsycn(IEnumerable<OrderItem> orderItems);
        Task<IEnumerable<OrderItem>> GetAsync(Expression<Func<OrderItem, bool>> predicate);
        Task UpdateAsync(IEnumerable<OrderItem> orderItems);
    }
}