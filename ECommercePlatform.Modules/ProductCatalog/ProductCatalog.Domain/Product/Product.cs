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

    public Result UpdateStock(int newQuantity)
    {
        Result<ProductStockQuantity> stockResult = ProductStockQuantity.Create(newQuantity);

        if (stockResult.IsFailed)
        {
            return Result.Fail(stockResult.Errors);
        }

        var previousQuantity = ProductStockQuantity.Value;

        ProductStockQuantity = stockResult.Value;

        UpdatedAt = DateTime.UtcNow;

        Raise(ProductStockUpdatedEvent.Create(
            Id.Value,
            previousQuantity,
            newQuantity));

        return Result.Ok();
    }

    public Result UpdateName(string name)
    {
        Result<ProductName> nameResult = ProductName.Create(name);

        if (nameResult.IsFailed)
        {
            return Result.Fail(nameResult.Errors);
        }

        Name = nameResult.Value;

        UpdatedAt = DateTime.UtcNow;

        return Result.Ok();
    }

    public Result UpdateImageUrl(Uri imageUrl)
    {
        Result<ProductImageUrl> imageUrlResult = ProductImageUrl.Create(imageUrl);

        if (imageUrlResult.IsFailed)
        {
            return Result.Fail(imageUrlResult.Errors);
        }

        ProductImageUrl = imageUrlResult.Value;

        UpdatedAt = DateTime.UtcNow;

        return Result.Ok();
    }

    public Result UpdatePrice(decimal price)
    {
        Result<ProductPrice> priceResult = ProductPrice.Create(price);

        if (priceResult.IsFailed)
        {
            return Result.Fail(priceResult.Errors);
        }

        Price = priceResult.Value;

        UpdatedAt = DateTime.UtcNow;

        return Result.Ok();
    }

    public Result UpdateDescription(string? description)
    {
        Result<ProductDescription> descriptionResult = ProductDescription.Create(description);

        if (descriptionResult.IsFailed)
        {
            return Result.Fail(descriptionResult.Errors);
        }

        ProductDescription = descriptionResult.Value;

        UpdatedAt = DateTime.UtcNow;

        return Result.Ok();
    }
}
