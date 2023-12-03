using OrderService.BLL.Repositories;
using OrderService.DAL.Filters;
using OrderService.DAL.OrderItems;
using OrderService.DAL.Orders;
using OrderService.DAL.Providers;

namespace OrderService.DAL.Infrastructure
{
    public class UoW : IUoW
    {
        readonly ApplicationContext _dbContext;

        IOrdersRepository? _ordersRepository;
        IOrderItemsRepository? _orderItemsRepository;
        IProvidersRepository? _providersRepository;
        IFiltersRepository _filtersRepository;

        public UoW(ApplicationContext dbContext) => _dbContext = dbContext;

        public IOrdersRepository OrdersRepository => _ordersRepository ??= new PostgresEfOrdersRepository(_dbContext);
        public IOrderItemsRepository OrderItemsRepository => _orderItemsRepository ??= new PostgresEfOrderItemsRepository(_dbContext);
        public IProvidersRepository ProvidersRepository => _providersRepository ??= new PostgresEfProvidersRepository(_dbContext);
        public IFiltersRepository FiltersRepository => _filtersRepository ??= new PostgresEfFiltersRepository(_dbContext);

        public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
