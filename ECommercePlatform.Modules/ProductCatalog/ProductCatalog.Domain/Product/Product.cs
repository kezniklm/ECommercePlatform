using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Events;
using ProductCatalog.Domain.Product.ValueObjects;
using SharedKernel;

namespace ProductCatalog.Domain.Product;

public sealed class Product : AggregateRoot<ProductId>
{
    private Product() { }

    private Product(
        ProductId id,
        ProductName name,
        ProductImageUrl productImageUrl,
        ProductPrice price,
        ProductDescription productDescription,
        ProductStockQuantity productStockQuantity)
        : base(id)
    {
        Name = name;
        ProductImageUrl = productImageUrl;
        Price = price;
        ProductDescription = productDescription;
        ProductStockQuantity = productStockQuantity;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public ProductName Name { get; private set; } = null!;
    public ProductImageUrl ProductImageUrl { get; private set; } = null!;
    public ProductPrice Price { get; private set; } = null!;
    public ProductDescription ProductDescription { get; private set; } = null!;
    public ProductStockQuantity ProductStockQuantity { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public bool IsAvailable => ProductStockQuantity.IsInStock;

    public static Result<Product> Create(string name, string imageLink)
    {
        Result<(ProductName, ProductImageUrl)> result = ProductName.Create(name)
            .Combine(ProductImageUrl.Create(imageLink));

        if (result.IsFailed)
        {
            return Result.Fail<Product>(result.Errors);
        }

        (ProductName productName, ProductImageUrl productImageUrl) = result.Value;

        return Create(
            ProductId.CreateUnique(),
            productName,
            productImageUrl,
            ProductPrice.Zero,
            ProductDescription.Empty,
            ProductStockQuantity.Zero);
    }

    public static Result<Product> Create(
        ProductId productId,
        ProductName productName,
        ProductImageUrl imageUrl,
        ProductPrice price,
        ProductDescription description,
        ProductStockQuantity stockQuantity)
    {
        var product = new Product(
            productId,
            productName,
            imageUrl,
            price,
            description,
            stockQuantity);

        product.Raise(new ProductCreatedEvent(Guid.NewGuid(), product));

        return product;
    }
}
