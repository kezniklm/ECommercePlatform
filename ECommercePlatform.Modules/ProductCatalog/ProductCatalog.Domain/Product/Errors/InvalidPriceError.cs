using System.Globalization;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class InvalidPriceError : ValidationError
{
    public InvalidPriceError(decimal price)
        : base($"Price must be between 0 and 999,999,999.99. Provided: {price.ToString(CultureInfo.InvariantCulture)}")
    {
        Metadata["code"] = "product.invalid_price";
        Metadata["price"] = price;
    }
}
