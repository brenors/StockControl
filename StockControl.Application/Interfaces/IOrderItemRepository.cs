using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;

namespace StockControl.Application.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
    }
}
