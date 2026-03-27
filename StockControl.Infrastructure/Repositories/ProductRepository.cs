using StockControl.Application.Interfaces;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
