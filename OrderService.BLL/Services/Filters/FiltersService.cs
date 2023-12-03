using OrderService.BLL.Infrastructure;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;

namespace OrderService.BLL.Services.Filters
{
    public class FiltersService : IFiltersService
    {
        readonly IUoW _uow;
        readonly IBackgroundDataHandler _backgroundDataHandler;

        public FiltersService(IUoW uow, IBackgroundDataHandler backgroundDataHandler)
        {
            _uow = uow;
            _backgroundDataHandler = backgroundDataHandler;
        }

        public async Task<Filter?> GetFiltersAsync()
        {
            Filter? filter = null;

            try
            {
                filter = await _uow.FiltersRepository.GetFiltersAsync();
            }
            catch (Exception ex)
            {
                ex.HandleException("Не удалось получить фильтры", () => _backgroundDataHandler.HandleLog(ex));
            }
            return filter;
        }
    }
}
