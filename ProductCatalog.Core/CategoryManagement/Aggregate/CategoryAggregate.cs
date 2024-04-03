using ProductCatalog.Core.Abstractions;
using ProductCatalog.Core.Shared;

namespace ProductCatalog.Core.CategoryManagement.Aggregate
{
    public class CategoryAggregate : DomainEntity<CategoryId>, IAggregateRoot
    {
        public string Name { get; private set; }

        public CategoryAggregate(string name)
        {
            Name = name;
        }
    }
}
