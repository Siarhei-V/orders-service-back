using Microsoft.AspNetCore.Mvc;
using OrderService.API.Dtos;
using OrderService.BLL.Services.OrderItems;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class OrderItemsController : ControllerBase
    {
        readonly IOrderItemsService _orderItemsService;

        public OrderItemsController(IOrderItemsService orderItemsService) => _orderItemsService = orderItemsService;

        [HttpGet("order-items")]
        public async Task<IActionResult> GetOrderItemsAsync(int orderId)
        {
            var items = await _orderItemsService.GetOrderItemsByOrderIdAsync(orderId);

            if (items?.Count() == 0)
                return NotFound(new BaseResponseDto { Status = StatusCodes.Status404NotFound, Message = "Нет данных" });

            return Ok(new ResponseDto { Message = "Элементы заказа успешно загружены", Data = items });
        }
    }
}
