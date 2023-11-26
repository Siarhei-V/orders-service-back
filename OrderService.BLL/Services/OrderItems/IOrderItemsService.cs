using OrderService.BLL.Models;

namespace OrderService.BLL.Services.OrderItems
{
    public interface IOrderItemsService
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    }
}