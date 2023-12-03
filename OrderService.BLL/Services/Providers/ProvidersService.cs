using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services.Providers
{
    public class ProvidersService : IProvidersService
    {
        readonly IUoW _uow;
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public ProvidersService(IUoW uow, IBackgroundDataHandler backgroundDataHandler)
        {
            _uow = uow;
            _backgroundDataHandler = backgroundDataHandler;
        }

        public async Task<IEnumerable<Provider>> GetProvidersAsync()
        {
            IEnumerable<Provider> providers = Enumerable.Empty<Provider>();

            try
            {
                providers = await _uow.ProvidersRepository.GetAsync();
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось получить поставщиков", () => _backgroundDataHandler.HandleLog(ex));
            }

            return providers;
        }
    }
}
