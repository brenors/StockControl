using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task AddOrderWithItemsAsync(Order order);
        Task<IEnumerable<Order>> FilterAsync(string? customerDocument, string? sellerName);
    }
}
