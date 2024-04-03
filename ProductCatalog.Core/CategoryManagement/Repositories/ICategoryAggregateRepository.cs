using ProductCatalog.Core.CategoryManagement.Aggregate;

namespace ProductCatalog.Core.CategoryManagement.Repositories
{
    public interface ICategoryAggregateRepository
    {
        public Task<CategoryAggregate?> GetById(int id);
        public Task<CategoryAggregate?> GetByName(string name);
        public Task Add(CategoryAggregate categoryAggregate);
        public Task Save();
    }
}
