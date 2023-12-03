using Microsoft.EntityFrameworkCore;
using OrderService.BLL.Models;
using OrderService.BLL.Repositories;
using OrderService.DAL.Infrastructure;
using System.Linq.Expressions;

namespace OrderService.DAL.Orders
{
    public class PostgresEfOrdersRepository : IOrdersRepository
    {
        readonly ApplicationContext _dbContext;

        public PostgresEfOrdersRepository(ApplicationContext dbContext) => _dbContext = dbContext;

        public async Task CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
        }

        public async Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _dbContext.Orders.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IEnumerable<Order> GetOrdersWithProvider(OrdersGettingRequestModel requestModel)
        {
            IQueryable<Order> query = _dbContext.Orders.Include(o => o.Provider).Include(o => o.OrderItems);
            
            var ordersByDate = query.Where(i => i.Date >= requestModel.DateFrom && i.Date <= requestModel.DateTo).ToList();

            IEnumerable<Order> concatOrders;

            IEnumerable<Order> ordersByNumbers = Enumerable.Empty<Order>();
            if (requestModel.NumbersFilter.Any())
                ordersByNumbers = ordersByDate.Where(o => requestModel.NumbersFilter.Any(i => i == o.Number));

            IEnumerable<Order> ordersByItemNumbers = Enumerable.Empty<Order>();
            if (requestModel.ItemNamesFilter.Any())
                ordersByItemNumbers = ordersByDate.Where(o => o.OrderItems.Any(i => requestModel.ItemNamesFilter.Any(f => f == i.Name)));

            IEnumerable<Order> ordersByItemUnits = Enumerable.Empty<Order>();
            if (requestModel.ItemUnitsFilter.Any())
                ordersByItemUnits = ordersByDate.Where(o => o.OrderItems.Any(i => requestModel.ItemUnitsFilter.Any(f => f == i.Unit)));

            IEnumerable<Order> ordersByProviderName = Enumerable.Empty<Order>();
            if (requestModel.ProviderNamesFilter.Any())
            {
                ordersByProviderName = ordersByDate.Where(o => requestModel.ProviderNamesFilter.Any(p => p == o.Provider.Name));
                if (!ordersByProviderName.Any() && !ordersByNumbers.Any() && !ordersByItemNumbers.Any() && !ordersByItemUnits.Any())
                    return ordersByProviderName;
            }

            concatOrders = ordersByNumbers.Union(ordersByItemNumbers).Union(ordersByItemUnits).Union(ordersByProviderName);
            
            var result = concatOrders.Any() ? concatOrders : ordersByDate;
            return result;
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
        }

        public async Task DeleteAsycn(Order order)
        {
            _dbContext.Orders.Remove(order);
        }
    }
}
