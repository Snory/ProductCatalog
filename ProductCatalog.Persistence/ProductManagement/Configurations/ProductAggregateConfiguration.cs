using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.ValueObjects;

namespace ProductCatalog.Persistence.ProductCatalogs.Configurations
{
    internal class ProductAggregateConfiguration : IEntityTypeConfiguration<ProductAggregate>
    {
        public void Configure(EntityTypeBuilder<ProductAggregate> builder)
        {
            ConfigureProduct(builder);
            ConfigureProductCategories(builder);
        }
        private void ConfigureProduct(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.ToTable("Products", "productsmanagement")
                   .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasConversion(
                        id => id.Value,
                        value => ProductId.Create(value)
                     )
                    .UseIdentityColumn()
                    .HasColumnName("Product_ID");


            builder.OwnsOne(e => e.ProductPrice, builder =>
            {
                builder.Property(p => p.CurrencyCode)
                       .HasConversion(
                            value => value.Code,
                            value => CurrencyCode.FromCode(value)
                        )
                       .HasMaxLength(3)
                       .HasColumnName("CurrencyCode");
              

                builder.Property(p => p.Price)
                       .HasColumnName("Price")
                       .HasPrecision(19, 3);
            });

            builder.Property(e => e.Ean)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(e => e.Description)
                   .IsRequired()
                   .HasMaxLength(255);

            /*
                Complex property cant be null and cant be owned 
            */

            builder.ComplexProperty(oi => oi.Quantity, qb =>
            {
                qb.Property(q => q.value)
                   .HasColumnName("Quantity");
            });
        }
        
        private void ConfigureProductCategories(EntityTypeBuilder<ProductAggregate> builder)
        {
            builder.OwnsMany(p => p.CategoryIds, c =>
            {
                c.ToTable("ProductCategories");

                c.WithOwner().HasForeignKey("Product_ID");

                c.HasKey("Id");

                c.Property(d => d.Value)
                 .HasColumnName("Category_ID")
                 .ValueGeneratedNever();
            });
        }
        
    }
}
