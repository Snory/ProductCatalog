using ProductCatalog.Core.Abstractions;

namespace ProductCatalog.Core.Shared
{
    public class CategoryId : ValueObject
    {
        private CategoryId() { }

        public int Value { get; private set; }

        private CategoryId(int value)
        {
            Value = value;
        }

        public static CategoryId Create(int id)
        {
            return new CategoryId(id);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
