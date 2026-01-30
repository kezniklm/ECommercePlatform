namespace ProductCatalog.Application.Commands.UpdateProductStock;

public record UpdateProductStockCommand(Guid ProductId, int NewStockQuantity)
{
    public record Result(Guid ProductId, int NewStockQuantity);
}
