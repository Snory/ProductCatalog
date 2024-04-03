using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Core.CategoryManagement.Repositories;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.Repositories;
using ProductCatalog.Persistence.CategoryManagement.Queries;
using ProductCatalog.Persistence.CategoryManagement.Repositories;
using ProductCatalog.Persistence.ProductManagement.Repositories;
using ProductCatalog.Persistence.ProductManagement.Repository;

namespace ProductCatalog.Persistence
{
    public static class PersistanceRegistrator
    {
        public static void RegisterEverything(IServiceCollection services)
        {
            services.AddDbContext<ProductCatalogContext>();
            services.AddScoped(typeof(IProductAggregateRepository<ProductAggregate>), typeof(EFProductAggregateRepository));
            services.AddScoped<ICategoryAggregateRepository, EFCategoryAggregateRepository>();
            services.AddScoped<IProductQuery, DapperProductQuery>();
            services.AddScoped<ICategoryQuery, DapperCategoryQuery>();
        }
    }
}