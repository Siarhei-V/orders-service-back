using Microsoft.AspNetCore.Mvc;
using OrderService.API.Dtos;
using OrderService.BLL.Services.Providers;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class ProvidersController : ControllerBase
    {
        readonly IProvidersService _providersService;

        public ProvidersController(IProvidersService providersService) => _providersService = providersService;

        [HttpGet("providers")]
        public async Task<IActionResult> GetProvidersAsync()
        {
            var result = await _providersService.GetProvidersAsync();

            if (!result.Any())
                return NotFound(new BaseResponseDto { Status = StatusCodes.Status404NotFound, Message = "Получен пустой список поставщиков" });

            return Ok(new ResponseDto { Message = "Список поставщиков успешно загружен", Data = result });
        }
    }
}
