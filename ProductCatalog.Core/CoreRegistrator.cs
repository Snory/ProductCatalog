using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Core.CategoryManagement.Factories;
using ProductCatalog.Core.ProductManagement.Factories;

namespace ProductCatalog.Core
{
    public static class CoreRegistrator
    {
        public static void RegisterEverything(IServiceCollection services)
        {
            services.AddScoped<IProductAggregateFactory, ProductAggregateFactory>();
            services.AddScoped<ICategoryAggregateFactory, CategoryAggregateFactory>();
        }
    }
}
