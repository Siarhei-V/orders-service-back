using Microsoft.VisualBasic;
using OrderService.BLL.CustomExceptions;
using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        readonly IOrdersRepository _ordersRepository;
        readonly IBackgroundDataHandler _backgroundDataHandler;
        readonly IRepository<Order> _ordersCommonRepository;
        readonly IOrderItemsRepository _itemsRepository;

        public OrdersService(IOrdersRepository ordersRepository, IBackgroundDataHandler backgroundDataHandler, IRepository<Order> ordersCommonRepository, IOrderItemsRepository itemsRepository)
        {
            _ordersRepository = ordersRepository;
            _backgroundDataHandler = backgroundDataHandler;
            _ordersCommonRepository = ordersCommonRepository;
            _itemsRepository = itemsRepository;
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
                var createdOrderId = await _ordersCommonRepository.CreateAsync(order);

                foreach (var item in orderItems)
                    item.OrderId = createdOrderId;

                await _itemsRepository.CreateAsync(orderItems);
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось сохранить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var deletedOrder = _ordersCommonRepository.Get(o => o.Id == id).FirstOrDefault();
                if (deletedOrder is null)
                    throw new CustomArgumentException("Запрашиваемого заказа не существует");

                await _ordersCommonRepository.DeleteAsync(new Order { Id = id });
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось удалить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }
    }
}
