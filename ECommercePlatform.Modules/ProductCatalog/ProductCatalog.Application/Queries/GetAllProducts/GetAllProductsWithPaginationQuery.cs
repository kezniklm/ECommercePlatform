namespace ProductCatalog.Application.Queries.GetAllProducts;

public record GetAllProductsWithPaginationQuery(int Page, int PageSize)
{
    public record Result(IReadOnlyList<ProductDto> Products, int Page, int PageSize);

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
