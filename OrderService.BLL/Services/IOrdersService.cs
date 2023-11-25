using OrderService.BLL.Dtos;
using OrderService.BLL.Models;

namespace OrderService.BLL.Services
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetForPeriod(OrdersGettingRequestModel requestModel);
    }
}