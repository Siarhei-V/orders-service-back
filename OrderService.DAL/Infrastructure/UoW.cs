using Microsoft.EntityFrameworkCore.Storage;
using OrderService.BLL.Repositories;
using OrderService.DAL.OrderItems;
using OrderService.DAL.Orders;
using OrderService.DAL.Providers;

namespace OrderService.DAL.Infrastructure
{
    // TODO: move SaveChangesAsync to uow
    public class UoW : IUoW
    {
        readonly ApplicationContext _dbContext;

        IDbContextTransaction? _transaction;

        IOrdersRepository? _ordersRepository;
        IOrderItemsRepository? _orderItemsRepository;
        IProvidersRepository? _providersRepository;

        public UoW(ApplicationContext dbContext) => _dbContext = dbContext;

        public IOrdersRepository OrdersRepository => _ordersRepository ??= new PostgresEfOrdersRepository(_dbContext);
        public IOrderItemsRepository OrderItemsRepository => _orderItemsRepository ??= new PostgresEfOrderItemsRepository(_dbContext);
        public IProvidersRepository ProvidersRepository => _providersRepository ??= new PostgresEfProvidersRepository(_dbContext);

        public async Task BeginTransactionAsync() => _transaction = await _dbContext.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
    }
}
