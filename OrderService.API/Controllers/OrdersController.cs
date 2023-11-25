using Microsoft.AspNetCore.Mvc;
using OrderService.API.Dtos;
using OrderService.BLL.Dtos;
using OrderService.BLL.Services;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class OrdersController : ControllerBase
    {
        readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService) => _ordersService = ordersService;

        [HttpGet("orders")]
        public IActionResult GetOrders(DateTime dateFrom, DateTime dateTo)
        {
            var result = _ordersService.GetForPeriod(new OrdersGettingRequestModel { DateFrom = dateFrom, DateTo = dateTo });
            return Ok(new ResponseDto { Message = "Заказы успешно загружены", Data = result });
        }
    }
}