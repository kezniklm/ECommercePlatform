using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Commands.UpdateProductStock;
using ProductCatalog.Presentation.Extensions;
using Wolverine;
using Wolverine.Http;

namespace ProductCatalog.Presentation.Endpoints.v2;

public static class UpdateProductStockEndpoint
{
    [WolverinePatch("/v2/products/{productId}/stock")]
    [Tags("ProductsV2")]
    [EndpointSummary("Update product stock (v2)")]
    [EndpointDescription("Updates the stock quantity of a product (v2)")]
    [ProducesResponseType(typeof(UpdateProductStockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public static async Task<IResult> UpdateStockV2(Guid productId, UpdateProductStockRequest request, IMessageBus bus)
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
