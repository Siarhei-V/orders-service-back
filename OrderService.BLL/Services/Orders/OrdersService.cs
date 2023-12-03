using OrderService.BLL.CustomExceptions;
using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        readonly IUoW _uow;
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public OrdersService(IBackgroundDataHandler backgroundDataHandler, IUoW uow)
        {
            _backgroundDataHandler = backgroundDataHandler;
            _uow = uow;
        }

        public IEnumerable<Order> GetForPeriod(OrdersGettingRequestModel requestModel)
        {
            IEnumerable<Order> orders = Enumerable.Empty<Order>();

            try
            {
                orders = _uow.OrdersRepository.GetOrdersWithProvider(i => i.Date >= requestModel.DateFrom && i.Date <= requestModel.DateTo);
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
                var orderCreationTask = _uow.OrdersRepository.CreateAsync(order);

                foreach (var item in orderItems)
                    item.Order = order;

                await orderCreationTask;
                await _uow.OrderItemsRepository.CreateAsync(orderItems);

                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось сохранить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }

        public async Task UpdateAsync(Order order, IEnumerable<OrderItem> orderItems)
        {
            try
            {
                var editedOrders = await _uow.OrdersRepository.GetAsync(o => o.Id ==  order.Id);
                var editedOrder = editedOrders.FirstOrDefault();
                if (editedOrder is null)
                    throw new CustomArgumentException("Запрашиваемого заказа не существует");

                await _uow.OrdersRepository.UpdateAsync(order);

                var oldItemsGettingTask = _uow.OrderItemsRepository.GetAsync(o => o.OrderId == order.Id);

                foreach (var item in orderItems)
                    item.OrderId = order.Id;

                var oldItems = await oldItemsGettingTask;

                // Если из списка товаров были удалены записи, удаляем лишнее из базы
                var itemsToRemove = oldItems.Where(o => orderItems.All(orderItem => orderItem.Id != o.Id)).ToList();
                if (itemsToRemove.Any())
                    await _uow.OrderItemsRepository.DeleteAsycn(itemsToRemove);

                // Если в новом списке и в базе равное количество товаров, обновляем записи
                var itemsToUpdate = orderItems.Where(o => o.Id != 0).ToList();
                if (itemsToUpdate.Any())
                    await _uow.OrderItemsRepository.UpdateAsync(itemsToUpdate);

                // Если в список товаров были добавлены записи, добавляем их в базу
                var itemsToCreate = orderItems.Where(o => o.Id == 0).ToList();
                if (itemsToCreate.Any()) 
                    await _uow.OrderItemsRepository.CreateAsync(itemsToCreate);

                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось обновить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var deletedOrders = await _uow.OrdersRepository.GetAsync(o => o.Id == id);
                var deletedOrder = deletedOrders.FirstOrDefault();
                if (deletedOrder is null)
                    throw new CustomArgumentException("Запрашиваемого заказа не существует");

                await _uow.OrdersRepository.DeleteAsycn(new Order { Id = id });
                await _uow.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось удалить заказ", () => _backgroundDataHandler.HandleLog(ex));
            }
        }
    }
}
