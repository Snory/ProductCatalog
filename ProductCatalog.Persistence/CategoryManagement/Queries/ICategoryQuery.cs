using ProductCatalog.Persistence.Utils;
using ProductCatalog.Structures;

namespace ProductCatalog.Persistence.CategoryManagement.Queries
{
    public interface ICategoryQuery
    {
        public Task<CategoryView?> GetByCategoryId(int id);
        public Task<(IEnumerable<CategoryView>, PaginationMetadata)> GetAll(int pageNumber = 1, int pageSize = 10);
    }
}
