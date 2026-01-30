using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class ProductNotFoundError : NotFoundError
{
    public ProductNotFoundError(Guid productId)
        : base($"Product with ID '{productId}' was not found.")
    {
        Metadata["code"] = "product.not_found";
        Metadata["productId"] = productId;
    }
}
