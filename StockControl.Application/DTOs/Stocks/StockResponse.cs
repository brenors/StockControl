using StockControl.Application.DTOs.Products;

namespace StockControl.Application.DTOs.Stocks
{
    public class StockResponse
    {
        public Guid Id { get; set; }
        public ProductResponse Product{ get; set; }
        public int Quantity { get; set; }
        public string? InvoiceNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
