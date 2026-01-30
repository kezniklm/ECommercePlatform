namespace ProductCatalog.Application.Commands.CreateProduct;

public record CreateProductCommand(CreateProductCommand.ProductData Product)
{
    public record Result(Guid ProductId);

    public record ProductData(string Name, string ImageUrl);
}
