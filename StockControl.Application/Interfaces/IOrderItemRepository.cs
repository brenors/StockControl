using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
    }
}
