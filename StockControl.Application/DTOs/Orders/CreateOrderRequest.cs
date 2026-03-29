namespace StockControl.Application.DTOs.Orders
{
    public class CreateOrderRequest
    {
        public string CustomerDocument { get; set; }
        public string SellerName { get; set; }

        public List<CreateOrderItemRequest> Items { get; set; }
    }

}
