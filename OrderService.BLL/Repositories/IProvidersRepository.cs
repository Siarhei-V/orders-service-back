using OrderService.BLL.Models;

namespace OrderService.BLL.Repositories
{
    public interface IProvidersRepository
    {
        Task<IEnumerable<Provider>> GetAsync();
    }
}