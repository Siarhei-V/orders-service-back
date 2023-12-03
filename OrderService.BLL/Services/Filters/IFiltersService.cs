using OrderService.BLL.Models;

namespace OrderService.BLL.Services.Filters
{
    public interface IFiltersService
    {
        Task<Filter?> GetFiltersAsync();
    }
}