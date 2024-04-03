using ProductCatalog.Core.Abstractions;
using ProductCatalog.Core.ProductCatalogs.ValueObjects;
using ProductCatalog.Core.ProductManagement.ValueObjects;
using ProductCatalog.Core.Shared;

namespace ProductCatalog.Core.ProductManagement.Aggregate
{
    public class ProductAggregate : DomainEntity<ProductId>, IAggregateRoot
    {
        private const int PRICE_UPDATE_LIMIT_HOURS = 12;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ProductAggregate() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal ProductAggregate(string ean, string description, Quantity quantity, MonetaryValue monetaryValue)
        {
            LastValueChange = DateTime.UtcNow;
            Ean = ean;
            Description = description;
            Quantity = quantity;
            ProductPrice = monetaryValue;
            _categoryIds = new();
        }
        public IReadOnlyList<CategoryId> CategoryIds => _categoryIds.AsReadOnly();

        public string Ean { get; private set; }
        public string Description { get; private set; }
        public Quantity Quantity { get; private set; }
        public MonetaryValue ProductPrice { get; private set; }

        public DateTimeOffset LastValueChange { get; private set; }

        private List<CategoryId> _categoryIds;

        public bool PriceOutdated => (DateTimeOffset.UtcNow - LastValueChange).TotalHours > PRICE_UPDATE_LIMIT_HOURS;

        public void ChangeProductPrice(MonetaryValue price)
        {
            if (!PriceOutdated)
            {
                throw new InvalidOperationException("Cannot change value sooner than 12 hours after last change");
            }

            ProductPrice = price;
        }

        public void UpdateProduct(string ean, string description, Quantity quantity, MonetaryValue monetaryValue)
        {
            ChangeProductPrice(monetaryValue);

            Ean = ean;
            Description = description;
            Quantity = quantity;
        }

        public void AddCategoryId(CategoryId categoryId)
        {
            if (!_categoryIds.Where(x => x == categoryId).Any())
            {
                _categoryIds.Add(categoryId);
            }
        }

        public void RemoveCategory(CategoryId categoryId)
        {
            var category = _categoryIds.Where(x => x == categoryId).FirstOrDefault();

            if(category != null)
            {
                _categoryIds.Remove(category);
            }
        }
    }
}
