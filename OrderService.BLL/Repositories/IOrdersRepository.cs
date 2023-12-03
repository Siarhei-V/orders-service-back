using OrderService.BLL.Models;
using System.Linq.Expressions;

namespace OrderService.BLL.Repositories
{
    public interface IOrdersRepository
    {
        Task CreateAsync(Order order);
        Task DeleteAsycn(Order order);
        Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate);
        IEnumerable<Order> GetOrdersWithProvider(OrdersGettingRequestModel requestModel);
        Task UpdateAsync(Order order);
    }
}
