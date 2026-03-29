namespace StockControl.Application.DTOs.Stocks
{
    public class StockEntryRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? InvoiceNumber { get; set; }
    }
}
