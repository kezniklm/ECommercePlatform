using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class InvalidDescriptionError : ValidationError
{
    public InvalidDescriptionError(int length)
        : base($"ProductDescription cannot exceed 4000 characters. Provided length: {length}")
    {
        Metadata["code"] = "product.invalid_description";
        Metadata["length"] = length;
    }
}
