using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.ProductManagement.Aggregate;
using ProductCatalog.Core.ProductManagement.Repositories;
using ProductCatalog.Core.ProductManagement.ValueObjects;

namespace ProductCatalog.Persistence.ProductManagement.Repository
{
    public class EFProductAggregateRepository : IProductAggregateRepository<ProductAggregate>
    {
        private ProductCatalogContext _context;

        public EFProductAggregateRepository(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task Add(ProductAggregate productAggregate)
        {
            await _context.AddAsync(productAggregate);
        }

        public async Task<ProductAggregate?> GetById(ProductId id)
        {
            return await _context.ProductAggregates.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductAggregate>> GetOutdatedProducts()
        {
            var collection = await _context.ProductAggregates.ToListAsync();

            return collection.Where(i => i.PriceOutdated);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
