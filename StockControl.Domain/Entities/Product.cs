namespace StockControl.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public ICollection<Stock> Stocks { get; set; }
    }
}
