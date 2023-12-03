using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;
using OrderService.BLL.Models;

namespace OrderService.DAL.Filters
{
    public class PostgresEfFiltersRepository : IFiltersRepository
    {
        readonly ApplicationContext _dbContext;

        public PostgresEfFiltersRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        public async Task<Filter> GetFiltersAsync()
        {
            var filter = new Filter
            {
                Numbers = await _dbContext.Orders.Select(o => o.Number).Distinct().ToListAsync(),
                ItemNames = await _dbContext.OrderItems.Select(o => o.Name).Distinct().ToListAsync(),
                ItemUnits = await _dbContext.OrderItems.Select(o => o.Unit).Distinct().ToListAsync(),
                ProviderNames = await _dbContext.Providers.Select(o => o.Name).Distinct().ToListAsync(),
            };

            return filter;
        }
    }
}
