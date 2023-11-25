namespace OrderService.BLL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        IEnumerable<T> Get(Func<T, bool> predicate);
        Task<IEnumerable<T>> GetAsync();
        Task UpdateAsync(T entity);
    }
}