using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Errors;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductDescription : ValueObject
{
    public const int MaxLength = 4000;

    private ProductDescription(string? value) => Value = value;

    public string? Value { get; }

    public static ProductDescription Empty => new(null);

    public bool HasValue => !string.IsNullOrWhiteSpace(Value);

    public static Result<ProductDescription> Create(string? description)
    {
        if (description is not null && description.Length > MaxLength)
        {
            return new InvalidDescriptionError(description.Length);
        }

        var finalDescription = description?.Trim().IsNullOrEmpty() == true ? null : description;

        return new ProductDescription(finalDescription);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
