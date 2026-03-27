namespace StockControl.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string CustomerDocument { get; set; }
        public string SellerName { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
