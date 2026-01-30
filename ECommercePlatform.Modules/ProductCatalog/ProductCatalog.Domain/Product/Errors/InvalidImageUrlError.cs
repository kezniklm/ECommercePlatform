using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class InvalidImageUrlError : ValidationError
{
    public InvalidImageUrlError(Uri? url, string reason) : base($"Invalid image URL: {reason}")
    {
        Metadata["code"] = "product.invalid_image_url";
        Metadata["url"] = url?.ToString() ?? "(null)";
        Metadata["reason"] = reason;
    }

    public InvalidImageUrlError(string reason) : base($"Invalid image URL: {reason}")
    {
        Metadata["code"] = "product.invalid_image_url";
        Metadata["reason"] = reason;
    }
}
