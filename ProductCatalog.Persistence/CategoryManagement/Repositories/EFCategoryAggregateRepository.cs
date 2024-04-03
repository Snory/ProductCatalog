using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.CategoryManagement.Aggregate;
using ProductCatalog.Core.CategoryManagement.Repositories;

namespace ProductCatalog.Persistence.CategoryManagement.Repositories
{
    public class EFCategoryAggregateRepository : ICategoryAggregateRepository
    {
        private ProductCatalogContext _context;

        public EFCategoryAggregateRepository(ProductCatalogContext context)
        {
            _context = context;
        }

        public async Task Add(CategoryAggregate categoryAggregate)
        {
            await _context.CategoryAggregates.AddAsync(categoryAggregate);
        }

        public async Task<CategoryAggregate?> GetById(int id)
        {
            return await _context.CategoryAggregates.Where(p => p.Id.Value == id).FirstOrDefaultAsync();
        }

        public async Task<CategoryAggregate?> GetByName(string name)
        {
            return await _context.CategoryAggregates.Where(p => p.Name == name).FirstOrDefaultAsync();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
