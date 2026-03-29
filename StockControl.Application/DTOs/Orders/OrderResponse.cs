namespace StockControl.Application.DTOs.Orders
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string CustomerDocument { get; set; }
        public string SellerName { get; set; }
        public decimal TotalAmount { get; set; }

        public List<OrderItemResponse> Items { get; set; }
    }
}
