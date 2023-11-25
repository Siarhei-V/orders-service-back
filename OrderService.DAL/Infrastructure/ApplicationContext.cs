using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;

namespace OrderService.DAL.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
