using ProductCatalog.Core.ProductCatalogs.ValueObjects;
using ProductCatalog.Core.ProductManagement.Aggregate;

namespace ProductCatalog.Core.ProductManagement.Factories
{
    public interface IProductAggregateFactory
    {
        public ProductAggregate CreateProductAggregateFrom(Quantity quantity, MonetaryValue productvalue, string ean, string description);
    }
}
