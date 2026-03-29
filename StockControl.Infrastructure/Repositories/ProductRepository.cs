using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Product>> GetByNameAsync(string name)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(x => x.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> FilterAsync(string? name, decimal? minPrice, decimal? maxPrice)
        {
            IQueryable<Product> query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.Name.Contains(name));
            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);
            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            return await query
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
