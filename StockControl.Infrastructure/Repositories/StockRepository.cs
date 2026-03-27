using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepository
    {
        public StockRepository(AppDbContext context) : base(context)
        {
        }
    }
}
