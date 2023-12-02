namespace OrderService.BLL.Repositories
{
    public interface IUoW
    {
        IOrderItemsRepository OrderItemsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IProvidersRepository ProvidersRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}