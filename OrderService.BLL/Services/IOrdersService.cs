using OrderService.BLL.Models;

namespace OrderService.BLL.Services
{
    public interface IOrdersService
    {
        Task CreateAsync(Order order, IEnumerable<OrderItem> orderItems);
        Task DeleteAsync(int id);
        IEnumerable<Order> GetForPeriod(OrdersGettingRequestModel requestModel);
    }
}