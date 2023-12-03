using OrderService.BLL.CustomExceptions;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.BL
{
    public class OrderValidator : IOrderValidator
    {
        readonly IUoW _uow;

        public OrderValidator(IUoW uow) => _uow = uow;

        async Task IOrderValidator.ValidateOrderAsync(Order order, IEnumerable<OrderItem> orderItems)
        {
            if (orderItems.Any(o => o.Name == order.Number))
                throw new CustomArgumentException("Имя заказа и номер заказа не могут совпадать");

            var existedOrder = await _uow.OrdersRepository.GetAsync(o => o.Number == order.Number && o.ProviderId == order.ProviderId);
            if (existedOrder.Any() && order.Id != existedOrder.First().Id)
                throw new CustomArgumentException("Заказ с такими номером и поставщиком уже существует");
        }
    }
}
