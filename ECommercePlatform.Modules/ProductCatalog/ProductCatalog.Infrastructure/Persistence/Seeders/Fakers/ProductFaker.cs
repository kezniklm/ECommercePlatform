using System.Globalization;
using Bogus;
using ProductCatalog.Domain.Product;
using ProductCatalog.Domain.Product.ValueObjects;

namespace ProductCatalog.Infrastructure.Persistence.Seeders.Fakers;

public sealed class ProductFaker : IFaker<Product>
{
    private readonly Faker<Product> _faker;

    public ProductFaker() => _faker = new Faker<Product>()
        .CustomInstantiator(f =>
            Product.Create(
                ProductId.CreateUnique(),
                ProductName.Create(f.Commerce.ProductName()).Value,
                ProductImageUrl.Create(f.Image.PicsumUrl()).Value,
                ProductPrice.Create(decimal.Parse(
                    f.Commerce.Price(5, 500),
                    CultureInfo.InvariantCulture)).Value,
                ProductDescription.Create(f.Commerce.ProductDescription()).Value,
                ProductStockQuantity.Create(f.Random.Int(0, 200)).Value
            ).Value
        );

    public IReadOnlyList<Product> Generate(int count)
        => _faker.Generate(count);
}
