using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Errors;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductName : ValueObject
{
    public const int MinLength = 1;

    public const int MaxLength = 200;

    private ProductName(string value) => Value = value;

    public string Value { get; }

    public static Result<ProductName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new InvalidProductNameError(name);
        }

        var trimmedName = name.Trim();

        if (trimmedName.Length is < MinLength or > MaxLength)
        {
            return new InvalidProductNameError(name);
        }

        return new ProductName(trimmedName);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
