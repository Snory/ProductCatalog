using ProductCatalog.ApiModel.CategoryManagement.WriteModel;
using ProductCatalog.ApiModel.ProductManagement.ReadModels;
using ProductCatalog.ApiModel.ProductManagement.WriteModels;
using ProductCatalog.Persistence.Utils;
using ProductCatalog.Structures;

namespace ProductCatalog.API.CategoryManagement.Facades
{
    public interface ICategoryManagementFacade
    {
        Task<int> CreateCategoryFrom(CreateCategoryInput categoryInput);
        Task<CategoryViewOut> GetCategoryById(int id);
        Task<(IEnumerable<CategoryViewOut>, PaginationMetadata)> GetAllCategories(int pageNumber = 1, int pageSize = 20);
    }
}
