using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services.OrderItems
{
    public class OrderItemsService : IOrderItemsService
    {
        readonly IUoW _uow;
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public OrderItemsService(IBackgroundDataHandler backgroundDataHandler, IUoW uow)
        {
            _backgroundDataHandler = backgroundDataHandler;
            _uow = uow;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            IEnumerable<OrderItem> orderItems = Enumerable.Empty<OrderItem>();

            try
            {
                orderItems = await _uow.OrderItemsRepository.GetAsync(o => o.OrderId == orderId);
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось получить элементы заказа", () => _backgroundDataHandler.HandleLog(ex));
            }

            return orderItems;
        }
    }
}
