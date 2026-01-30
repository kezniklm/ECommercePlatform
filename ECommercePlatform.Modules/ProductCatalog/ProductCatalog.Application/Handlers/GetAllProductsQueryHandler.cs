using ProductCatalog.Application.Queries.GetAllProducts;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Application.Handlers;

public sealed class GetAllProductsQueryHandler
{
    public static async Task<GetAllProductsQuery.Result> Handle(
        GetAllProductsQuery _,
        IQueryObject<Product> queryObject)
    {
        IEnumerable<Product> products = await queryObject
            .OrderBy(p => p.CreatedAt, false)
            .ExecuteAsync();

        List<GetAllProductsQuery.ProductDto> productDtos = products.Select(p => new GetAllProductsQuery.ProductDto(
            p.Id.Value,
            p.Name.Value,
            p.ProductImageUrl.Value.ToString(),
            p.Price.Amount,
            p.ProductDescription.Value,
            p.ProductStockQuantity.Value,
            p.IsAvailable,
            p.CreatedAt,
            p.UpdatedAt)).ToList();

        return new GetAllProductsQuery.Result(productDtos);
    }
}
