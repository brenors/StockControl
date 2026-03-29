using StockControl.Domain.Entities;

namespace StockControl.Application.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetByNameAsync(string name);
        Task<IEnumerable<Product>> FilterAsync(string? name, decimal? minPrice, decimal? maxPrice);
    }
}
