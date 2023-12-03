using OrderService.BLL.Models;

namespace OrderService.BLL.Repositories
{
    public interface ILogsRepository
    {
        Task CreateAsync(Log logModel);
    }
}