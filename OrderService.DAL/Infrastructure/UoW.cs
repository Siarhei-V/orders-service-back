using Microsoft.EntityFrameworkCore.Storage;
using OrderService.BLL.Repositories;
using OrderService.DAL.Orders;

namespace OrderService.DAL.Infrastructure
{
    public class UoW : IUoW
    {
        readonly ApplicationContext _dbContext;

        IOrdersRepository? _ordersRepository;
        IOrderItemsRepository? _orderItemsRepository;
        IDbContextTransaction? _transaction;

        public UoW(ApplicationContext dbContext) => _dbContext = dbContext;

        public IOrdersRepository OrdersRepository => _ordersRepository ??= new PostgresEfOrdersRepository(_dbContext);
        public IOrderItemsRepository OrderItemsRepository => _orderItemsRepository ??= new PostgresEfOrderItemsRepository(_dbContext);

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
