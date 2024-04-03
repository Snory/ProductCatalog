using ProductCatalog.Core.CategoryManagement.Aggregate;
using ProductCatalog.Core.CategoryManagement.Repositories;

namespace ProductCatalog.Core.CategoryManagement.Factories
{
    public class CategoryAggregateFactory : ICategoryAggregateFactory
    {
        private readonly ICategoryAggregateRepository _repository;

        public CategoryAggregateFactory(ICategoryAggregateRepository repository)
        {
            _repository = repository;
        }

        public CategoryAggregate CreateCategoryAggregateFrom(string name)
        {
            if (_repository.GetByName(name).Result is not null)
            {
                throw new InvalidOperationException("Category with this name already exists");
            }

            return new CategoryAggregate(name);
        }
    }
}
