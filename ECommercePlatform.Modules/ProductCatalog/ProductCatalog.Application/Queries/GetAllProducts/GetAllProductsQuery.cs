namespace ProductCatalog.Application.Queries.GetAllProducts;

public record GetAllProductsQuery
{
    public record Result(IReadOnlyList<ProductDto> Products);

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
