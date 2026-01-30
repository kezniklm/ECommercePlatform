namespace ProductCatalog.Application.Queries.GetProductById;

public record GetProductQuery(Guid ProductId)
{
    public record Result(ProductDto Product);

    public record ProductDto(
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
