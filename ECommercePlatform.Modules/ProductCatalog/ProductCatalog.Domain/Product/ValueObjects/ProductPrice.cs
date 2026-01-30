using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Errors;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductPrice : ValueObject
{
    public const decimal MinValue = 0m;

    public const decimal MaxValue = 999_999_999.99m;

    private ProductPrice(decimal amount) => Amount = amount;

    public decimal Amount { get; }

    public static ProductPrice Zero => new(0m);

    public static Result<ProductPrice> Create(decimal amount)
    {
        if (amount is < MinValue or > MaxValue)
        {
            return new InvalidPriceError(amount);
        }

        return new ProductPrice(Math.Round(amount, 2));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Amount;
    }

    public override string ToString() => $"{Amount:F2}";
}
