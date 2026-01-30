using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductId : ValueObject
{
    private ProductId(Guid value) => Value = value;

    public Guid Value { get; }

    public static ProductId Create(Guid id) => new(id);

    public static ProductId CreateUnique() => new(Guid.NewGuid());

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
