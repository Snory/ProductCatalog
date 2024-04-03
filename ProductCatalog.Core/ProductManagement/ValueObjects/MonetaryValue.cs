using ProductCatalog.Core.Abstractions;
using ProductCatalog.Core.ProductManagement.ValueObjects;

namespace ProductCatalog.Core.ProductCatalogs.ValueObjects
{
    public class MonetaryValue : ValueObject
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private MonetaryValue() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public MonetaryValue(CurrencyCode currencyCode, decimal amount)
        {
            CurrencyCode = currencyCode;
            Price = amount;
        }

        public CurrencyCode CurrencyCode { get; }
        public decimal Price { get;}        


        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
