using ProductCatalog.Application.Queries.GetAllProducts;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Application.Handlers;

public sealed class GetAllProductsWithPaginationQueryHandler
{
    public static async Task<GetAllProductsWithPaginationQuery.Result> Handle(
        GetAllProductsWithPaginationQuery query,
        IQueryObject<Product> queryObject)
    {
        IEnumerable<Product> pagedProducts = await queryObject.Page(query.Page, query.PageSize).ExecuteAsync();

        var productDtos = pagedProducts.Select(p =>
            new GetAllProductsWithPaginationQuery.ProductDto(
                p.Id.Value,
                p.Name.Value,
                p.ProductImageUrl.Value.ToString(),
                p.Price.Amount,
                p.ProductDescription.Value,
                p.ProductStockQuantity.Value,
                p.IsAvailable,
                p.CreatedAt,
                p.UpdatedAt)).ToList();
        
            return new GetAllProductsWithPaginationQuery.Result(productDtos, query.Page, query.PageSize);
    }
}
