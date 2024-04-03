namespace ProductCatalog.Core.Abstractions
{
    public abstract class DomainEntity<Tid>
    {
        public Tid Id { get; private set; } = default!;
    }
}
