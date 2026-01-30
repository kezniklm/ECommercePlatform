using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Queries.GetAllProducts;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v1;

public static class GetAllProductsEndpoint
{
    [WolverineGet("/products")]
    [Tags("ProductsV1")]
    [EndpointSummary("Get all products")]
    [EndpointDescription("Returns a list of all available products")]
    [ProducesResponseType(typeof(GetAllProductsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> GetAllV1(IMessageBus bus)
    {
        var query = new GetAllProductsQuery();

        var result = await bus.InvokeAsync<GetAllProductsQuery.Result>(query);

        return Results.Ok(new GetAllProductsResponse(result.Products
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
            .ToList()));
    }

    public record GetAllProductsResponse(IReadOnlyList<ProductResponse> Products);

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
