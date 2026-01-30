using System.Globalization;
using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Errors;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductStockQuantity : ValueObject
{
    public const int MinValue = 0;

    public const int MaxValue = 999_999_999;

    private ProductStockQuantity(int value) => Value = value;

    public int Value { get; }

    public static ProductStockQuantity Zero => new(0);

    public bool IsInStock => Value > 0;

    public static Result<ProductStockQuantity> Create(int quantity)
    {
        if (quantity is < MinValue or > MaxValue)
        {
            return new InvalidStockQuantityError(quantity);
        }

        return new ProductStockQuantity(quantity);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
