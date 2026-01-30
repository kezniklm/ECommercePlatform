using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Commands.CreateProduct;
using ProductCatalog.Presentation.Extensions;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v2;

public static class CreateProductEndpoint
{
    [WolverinePost("/v2/products")]
    [Tags("ProductsV2")]
    [EndpointSummary("Create a new product (v2)")]
    [EndpointDescription("Creates a new product with the specified name and image URL (v2)")]
    [ProducesResponseType(typeof(CreateProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> CreateV2(CreateProductRequest request, IMessageBus bus)
    {
        var command = new CreateProductCommand(
            new CreateProductCommand.ProductData(request.Name, request.ImageUrl));

        var result = await bus.InvokeAsync<Result<CreateProductCommand.Result>>(command);

        return result switch
        {
            { IsSuccess: true } => Results.Created($"/v2/products/{result.Value.ProductId}",
                new CreateProductResponse(result.Value.ProductId)),
            { IsSuccess: false } => result.Errors.ToHttpResult()
        };
    }

    public record CreateProductRequest(string Name, string ImageUrl);

    public record CreateProductResponse(Guid ProductId);
}
