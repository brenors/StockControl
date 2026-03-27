using StockControl.Application.Repositories;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
