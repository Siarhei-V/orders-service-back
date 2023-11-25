using OrderService.BLL.Dtos;
using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services
{
    public class OrdersService : IOrdersService
    {
        readonly IOrdersRepository _ordersRepository;
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public OrdersService(IOrdersRepository ordersRepository, IBackgroundDataHandler backgroundDataHandler)
        {
            _ordersRepository = ordersRepository;
            _backgroundDataHandler = backgroundDataHandler;
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
                ex.HandleException("Не удалось получить заказы", () => _backgroundDataHandler.HandleLog(ex, 3));
            }

            return orders;
        }
    }
}
