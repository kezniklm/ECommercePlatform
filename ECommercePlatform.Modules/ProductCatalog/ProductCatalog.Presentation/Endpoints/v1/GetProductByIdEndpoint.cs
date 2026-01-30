using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Queries.GetProductById;
using ProductCatalog.Presentation.Extensions;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v1;

public static class GetProductByIdEndpoint
{
    [WolverineGet("/products/{productId}")]
    [Tags("ProductsV1")]
    [EndpointSummary("Get a product by ID")]
    [EndpointDescription("Returns a single product by its unique identifier")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> GetByIdV1(Guid productId, IMessageBus bus)
    {
        var query = new GetProductQuery(productId);

        var result = await bus.InvokeAsync<Result<GetProductQuery.Result>>(query);

        return result switch
        {
            { IsSuccess: true } => Results.Ok(new ProductResponse(
                result.Value.Product.Id,
                result.Value.Product.Name,
                result.Value.Product.ImageUrl,
                result.Value.Product.Price,
                result.Value.Product.Description,
                result.Value.Product.StockQuantity,
                result.Value.Product.IsAvailable,
                result.Value.Product.CreatedAt,
                result.Value.Product.UpdatedAt)),
            { IsSuccess: false } => result.Errors.ToHttpResult()
        };
    }

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
