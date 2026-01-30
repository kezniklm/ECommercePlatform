using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Queries.GetAllProducts;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v2;

public static class GetAllProductsEndpoint
{
    [WolverineGet("/v2/products")]
    [Tags("ProductsV2")]
    [EndpointSummary("Get all products (paginated)")]
    [EndpointDescription("Returns a paginated list of all available products (v2)")]
    [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> GetAllV2(IMessageBus bus, int page = 1, int pageSize = 10)
    {
        var query = new GetAllProductsWithPaginationQuery(page, pageSize);

        var result = await bus.InvokeAsync<GetAllProductsWithPaginationQuery.Result>(query);

        List<ProductResponse> products = result.Products
            .Select(p => new ProductResponse(
                p.Id,
                p.Name,
                p.ImageUrl,
                p.Price,
                p.Description,
                p.StockQuantity,
                p.IsAvailable,
                p.CreatedAt,
                p.UpdatedAt))
            .ToList();

        var response = new GetAllProductsResponse(products, result.Page, result.PageSize);

        return Results.Ok(response);
    }

    public record GetAllProductsResponse(
        IReadOnlyList<ProductResponse> Products,
        int Page,
        int PageSize);

    public record ProductResponse(
        Guid Id,
        string Name,
        string ImageUrl,
        decimal Price,
        string? Description,
        int StockQuantity,
        bool IsAvailable,
        DateTime CreatedAt,
        DateTime UpdatedAt);
}
