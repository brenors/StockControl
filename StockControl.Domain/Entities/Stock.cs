namespace StockControl.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
