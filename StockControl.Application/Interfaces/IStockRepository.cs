using StockControl.Domain.Entities;

namespace StockControl.Application.Interfaces
{
    public interface IStockRepository : IRepository<Stock>
    {
        Task<int> GetAvailableQuantity(Guid productId);
        Task DecreaseStock(Guid productId, int quantity);
    }
}
