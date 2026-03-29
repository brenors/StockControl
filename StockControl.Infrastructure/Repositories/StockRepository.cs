using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetAvailableQuantity(Guid productId)
        {
            return await _dbSet
                .Where(x => x.ProductId == productId)
                .SumAsync(x => x.Quantity);
        }

        public async Task DecreaseStock(Guid productId, int quantity)
        {
            var stocks = await _dbSet
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();

            var remaining = quantity;

            foreach (var stock in stocks)
            {
                if (remaining <= 0) break;

                if (stock.Quantity <= remaining)
                {
                    remaining -= stock.Quantity;
                    stock.Quantity = 0;
                }
                else
                {
                    stock.Quantity -= remaining;
                    remaining = 0;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
