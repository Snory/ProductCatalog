using ProductCatalog.Core.ProductCatalogs.ValueObjects;
using ProductCatalog.Core.ProductManagement.Aggregate;

namespace ProductCatalog.Core.ProductManagement.Factories
{
    public sealed class ProductAggregateFactory : IProductAggregateFactory
    {
        public ProductAggregate CreateProductAggregateFrom(Quantity quantity, MonetaryValue productvalue, string ean, string description)
        {
            return new ProductAggregate(ean, description, quantity, productvalue);
        }
    }
}
