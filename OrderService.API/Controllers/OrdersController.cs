using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderService.API.Dtos;
using OrderService.BLL.Models;
using OrderService.BLL.Services.Orders;

namespace OrderService.API.Controllers
{
    // TODO: add healthcheck
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

            if (result?.Count() == 0)
                return NotFound(new ResponseDto { Status = StatusCodes.Status404NotFound, Message = "За указанный период нет заказов" });

            return Ok(new ResponseDto { Message = "Заказы успешно загружены", Data = result });
        }

        [HttpPost("orders/order")]
        public async Task<IActionResult> CreateOrderAsync([FromBody] OrderCreationDto orderCreationDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderCreationDto, Order>()).CreateMapper();
            var order = mapper.Map<Order>(orderCreationDto);

            mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderItemCreationModel, OrderItem>()).CreateMapper();
            var orderItems = mapper.Map<IEnumerable<OrderItemCreationModel>, IEnumerable<OrderItem>>(orderCreationDto.OrderItems);

            await _ordersService.CreateAsync(order, orderItems);
            
            var responseDto = new ResponseDto { Status = StatusCodes.Status201Created, Message = "Заказ успешно сохранен" };
            return StatusCode(StatusCodes.Status201Created, responseDto);
        }

        [HttpDelete("orders/order/{id}")]
        public async Task<IActionResult> DeleteOrderAsync(int id)
        {
            await _ordersService.DeleteAsync(id);
            return Ok(new ResponseDto { Message = "Заказ успешно удален" });
        }
    }
}