using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services.OrderItems
{
    public class OrderItemsService : IOrderItemsService
    {
        readonly IBackgroundDataHandler _backgroundDataHandler;
        readonly IRepository<OrderItem> _orderItemsRepository;

        public OrderItemsService(IBackgroundDataHandler backgroundDataHandler, IRepository<OrderItem> orderItemsRepository)
        {
            _backgroundDataHandler = backgroundDataHandler;
            _orderItemsRepository = orderItemsRepository;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            IEnumerable<OrderItem> orderItems = Enumerable.Empty<OrderItem>();

            try
            {
                orderItems = await _orderItemsRepository.GetAsync(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось получить элементы заказа", () => _backgroundDataHandler.HandleLog(ex));
            }

            return orderItems;
        }
    }
}
