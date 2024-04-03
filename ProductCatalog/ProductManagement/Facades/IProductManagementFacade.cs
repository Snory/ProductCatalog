using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.ApiModel.ProductManagement.WriteModels;
using ProductCatalog.Persistence.Utils;

namespace ProductCatalog.API.ProductManagement.Facades
{
    public interface IProductManagementFacade
    {
        Task<int> CreateProductFrom(CreateProductInput input);
        Task UpdateProduct(UpdateProductInput input);
        Task AddCategory(AddCategoryInput input);
        Task RemoveCategory(RemoveCategoryInput input);
        Task<(IEnumerable<ProductViewOut>, PaginationMetadata)> GetAllProducts(string? categoryFilter = null, int pageNumber = 1, int pageSize = 20);
        Task<ProductViewOut> GetProductById(int id);
        Task RefreshPrices(CancellationToken stoppingToken);
    }
}
