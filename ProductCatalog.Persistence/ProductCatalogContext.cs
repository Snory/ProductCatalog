using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductCatalog.Core.CategoryManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Persistence.Configuration;

namespace ProductCatalog.Persistence
{
    public class ProductCatalogContext : DbContext
    {
        public DbSet<ProductAggregate> ProductAggregates { get; set;}
        public DbSet<CategoryAggregate> CategoryAggregates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ProductCatalog", builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(1), null);
            });

            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductCatalogContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
