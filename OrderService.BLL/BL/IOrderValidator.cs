using OrderService.BLL.Models;

namespace OrderService.BLL.BL
{
    public interface IOrderValidator
    {
        internal Task ValidateOrderAsync(Order order, IEnumerable<OrderItem> orderItems);
    }
}