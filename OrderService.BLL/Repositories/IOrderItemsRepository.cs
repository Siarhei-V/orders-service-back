using OrderService.BLL.Models;

namespace OrderService.BLL.Repositories
{
    public interface IOrderItemsRepository
    {
        Task CreateAsync(IEnumerable<OrderItem> orderItems);
    }
}