namespace OrderService.BLL.Repositories
{
    public interface IUoW
    {
        IOrderItemsRepository OrderItemsRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        IProvidersRepository ProvidersRepository { get; }
        IFiltersRepository FiltersRepository { get; }

        Task SaveChangesAsync();
    }
}