using OrderService.BLL.Models;

namespace OrderService.BLL.Repositories
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetOrdersWithProvider(Func<Order, bool> predicate);
    }
}
