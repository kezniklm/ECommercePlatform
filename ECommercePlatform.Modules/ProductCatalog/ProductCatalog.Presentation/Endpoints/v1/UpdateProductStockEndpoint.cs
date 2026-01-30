using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Commands.UpdateProductStock;
using ProductCatalog.Presentation.Extensions;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v1;

public static class UpdateProductStockEndpoint
{
    [WolverinePatch("/products/{productId}/stock")]
    [Tags("ProductsV1")]
    [EndpointSummary("Update product stock")]
    [EndpointDescription("Updates the stock quantity of a product")]
    [ProducesResponseType(typeof(UpdateProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> UpdateStockV1(Guid productId, UpdateProductStockRequest request, IMessageBus bus)
    {
        var command = new UpdateProductStockCommand(productId, request.NewStockQuantity);

        var result = await bus.InvokeAsync<Result<UpdateProductStockCommand.Result>>(command);

        return result switch
        {
            { IsSuccess: true } => Results.Ok(new UpdateProductStockResponse(
                result.Value.ProductId,
                result.Value.NewStockQuantity)),
            { IsSuccess: false } => result.Errors.ToHttpResult()
        };
    }

    public record UpdateProductStockRequest(int NewStockQuantity);

    public record UpdateProductStockResponse(Guid ProductId, int NewStockQuantity);
}
