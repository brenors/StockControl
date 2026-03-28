using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;

namespace StockControl.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
    }
}
