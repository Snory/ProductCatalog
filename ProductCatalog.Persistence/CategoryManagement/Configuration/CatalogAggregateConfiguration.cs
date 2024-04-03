using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.CategoryManagement.Aggregate;
using ProductCatalog.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Persistence.CategoryManagement.Configuration
{
    public class CatalogAggregateConfiguration : IEntityTypeConfiguration<CategoryAggregate>
    {
        public void Configure(EntityTypeBuilder<CategoryAggregate> builder)
        {
            builder.ToTable("Categories", "categoriesmanagement")
                   .HasKey(e => e.Id);

            builder.Property(e => e.Id)
                    .HasConversion(
                        id => id.Value,
                        value => CategoryId.Create(value)
                     )
                    .UseIdentityColumn()
                    .HasColumnName("Category_ID");

            builder.Property(e => e.Name)
                   .HasMaxLength(100)
                   .IsRequired()
                   .IsUnicode(true);

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
