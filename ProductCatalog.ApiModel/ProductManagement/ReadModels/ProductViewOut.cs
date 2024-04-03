namespace ProductCatalog.ApiModel.ProductManagement.ReadModels
{
    public class ProductViewOut
    {
        public int ProductId { get; set; }
        public required string Ean { get; set; }
        public required string ProductDescription { get; set; }
        public required string CurrencyCode { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset LastValueChange { get; set; }
        public List<CategoryViewOut> Categories { get; set; } = [];
    }
}
