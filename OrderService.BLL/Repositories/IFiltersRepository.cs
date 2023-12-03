using OrderService.BLL.Models;

namespace OrderService.BLL.Repositories
{
    public interface IFiltersRepository
    {
        Task<Filter> GetFiltersAsync();
    }
}