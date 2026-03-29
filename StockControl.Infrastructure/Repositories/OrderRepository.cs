using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddOrderWithItemsAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> FilterAsync(string? customerDocument, string? sellerName)
        {
            IQueryable<Order> query = _dbSet
                .Include(o => o.Items)
                    .ThenInclude(oi => oi.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(customerDocument))
            {
                query = query.Where(o => o.CustomerDocument.Contains(customerDocument));
            }

            if (!string.IsNullOrWhiteSpace(sellerName))
            {
                query = query.Where(o => o.SellerName.Contains(sellerName));
            }

            return await query
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
