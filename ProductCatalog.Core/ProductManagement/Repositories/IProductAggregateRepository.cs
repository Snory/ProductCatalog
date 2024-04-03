using ProductCatalog.Core.Abstractions;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.ValueObjects;

namespace ProductCatalog.Core.ProductManagement.Repositories
{
    public interface IProductAggregateRepository<T> where T : IAggregateRoot
    {
        public Task<T?> GetById(ProductId id);
        public Task Add(T productAggregate);
        public Task Save();
        public Task<IEnumerable<T>> GetOutdatedProducts();

    }
}
