using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        readonly IOrdersRepository _ordersRepository;
        readonly IBackgroundDataHandler _backgroundDataHandler;
        readonly IRepository<Order> _orderRepository;
        readonly IOrderItemsRepository _itemRepository;

        public OrdersService(IOrdersRepository ordersRepository, IBackgroundDataHandler backgroundDataHandler, IRepository<Order> orderRepository, IOrderItemsRepository itemRepository)
        {
            _ordersRepository = ordersRepository;
            _backgroundDataHandler = backgroundDataHandler;
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
        }

        public IEnumerable<Order> GetForPeriod(OrdersGettingRequestModel requestModel)
        {
            IEnumerable<Order> orders = Enumerable.Empty<Order>();

            try
            {
                orders = _ordersRepository.GetOrdersWithProvider(i => i.Date >= requestModel.DateFrom && i.Date <= requestModel.DateTo);
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось получить заказы", () => _backgroundDataHandler.HandleLog(ex));
            }

            return orders;
        }

        public async Task CreateAsync(Order order, IEnumerable<OrderItem> orderItems)
        {
            try
            {
                var createdOrderId = await _orderRepository.CreateAsync(order);

                foreach (var item in orderItems)
                    item.OrderId = createdOrderId;

                await _itemRepository.CreateAsync(orderItems);
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось сохранить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }
    }
}
