using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class InvalidProductNameError : ValidationError
{
    public InvalidProductNameError(string? name)
        : base("Product name cannot be empty and must be between 1 and 200 characters.")
    {
        Metadata["code"] = "product.invalid_name";
        Metadata["name"] = name ?? "(null)";
    }
}
