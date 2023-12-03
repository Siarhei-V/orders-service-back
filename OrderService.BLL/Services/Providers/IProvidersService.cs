using OrderService.BLL.Models;

namespace OrderService.BLL.Services.Providers
{
    public interface IProvidersService
    {
        Task<IEnumerable<Provider>> GetProvidersAsync();
    }
}