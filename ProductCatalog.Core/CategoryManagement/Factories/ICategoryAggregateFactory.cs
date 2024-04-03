using ProductCatalog.Core.CategoryManagement.Aggregate;
using ProductCatalog.Core.ProductCatalogs.ValueObjects;
using ProductCatalog.Core.ProductManagement.Aggregate;

namespace ProductCatalog.Core.CategoryManagement.Factories
{
    public interface ICategoryAggregateFactory
    {
        public CategoryAggregate CreateCategoryAggregateFrom(string name);
    }
}
