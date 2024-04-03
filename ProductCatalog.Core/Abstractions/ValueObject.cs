namespace ProductCatalog.Core.Abstractions
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        private bool ValuesAreEqual(ValueObject valueObject) =>
            GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());

        public override bool Equals(object? obj) =>
            obj is ValueObject valueObject && ValuesAreEqual(valueObject);

        public override int GetHashCode()
        {
            return GetEqualityComponents().Aggregate(
            default(int),
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));
        }

        public bool Equals(ValueObject? other) =>
            other is not null && ValuesAreEqual(other);

        public static bool operator ==(ValueObject? a, ValueObject? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            if (a is null || b is null)
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject? a, ValueObject? b) => !(a == b);
    }
}
