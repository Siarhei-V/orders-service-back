using Microsoft.AspNetCore.Mvc;
using OrderService.API.Dtos;
using OrderService.BLL.Services.Filters;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class FiltersController : ControllerBase
    {
        readonly IFiltersService _filtersService;

        public FiltersController(IFiltersService filtersService) => _filtersService = filtersService;

        [HttpGet("filters")]
        public async Task<IActionResult> GetFiltersAsync()
        {
            var result = await _filtersService.GetFiltersAsync();

            if (result == null) 
                return NotFound(new BaseResponseDto { Status = StatusCodes.Status404NotFound, Message = "Не удалось получить фильтры" });

            return Ok(new ResponseDto { Message = "Фильтры успешно загружены", Data = result });
        }
    }
}
