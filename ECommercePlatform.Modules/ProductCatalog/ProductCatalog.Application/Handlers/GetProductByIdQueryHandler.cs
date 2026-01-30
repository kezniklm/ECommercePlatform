using FluentResults;
using ProductCatalog.Application.Queries.GetProductById;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;
using ProductCatalog.Domain.Product.Errors;
using ProductCatalog.Domain.Product.ValueObjects;

namespace ProductCatalog.Application.Handlers;

public sealed class GetProductByIdQueryHandler
{
    public static async Task<Result<GetProductQuery.Result>> Handle(
        GetProductQuery query,
        IQueryObject<Product> queryObject)
    {
        IEnumerable<Product> products = await queryObject
            .Filter(p => p.Id == ProductId.Create(query.ProductId))
            .ExecuteAsync();

        var product = products.FirstOrDefault();

        if (product is null)
        {
            return Result.Fail<GetProductQuery.Result>(new ProductNotFoundError(query.ProductId));
        }

        var productDto = new GetProductQuery.ProductDto(
            product.Id.Value,
            product.Name.Value,
            product.ProductImageUrl.Value.ToString(),
            product.Price.Amount,
            product.ProductDescription.Value,
            product.ProductStockQuantity.Value,
            product.IsAvailable,
            product.CreatedAt,
            product.UpdatedAt);

        return new GetProductQuery.Result(productDto);
    }
}
