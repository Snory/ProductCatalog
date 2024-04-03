using ProductCatalog.Core.Abstractions;

namespace ProductCatalog.Core.ProductManagement.ValueObjects
{
    public class ProductId : ValueObject
    {
        public static readonly ProductId NEW = new ProductId(default);

        private ProductId() { }

        private ProductId(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public static ProductId Create(int id)
        {
            return new ProductId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
