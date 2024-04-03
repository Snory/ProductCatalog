using ProductCatalog.Persistence.Utils;
using ProductCatalog.Structures;

namespace ProductCatalog.Persistence.ProductManagement.Repositories
{
    public interface IProductQuery
    {
        public Task<(IEnumerable<ProductView>, PaginationMetadata)> GetAll(string? categoryFilter = null, int pageNumber = 1, int pageSize = 10);
        public Task<ProductView?> GetProductById(int id);
    }
}
