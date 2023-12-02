using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;

namespace OrderService.DAL.Providers
{
    public class PostgresEfProvidersRepository : IProvidersRepository
    {
        readonly ApplicationContext _dbContext;

        public PostgresEfProvidersRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<Provider>> GetAsync()
        {
            return await _dbContext.Providers.AsNoTracking().ToListAsync();
        }
    }
}
