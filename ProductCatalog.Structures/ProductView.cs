namespace ProductCatalog.Structures
{
    public class ProductView
    {
        public int ProductId { get; set; }
        public required string Ean {  get; set; }
        public required string ProductDescription { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset LastValueChange { get; set; }
        public List<CategoryView> Categories { get; set; } = [];
    }
}
