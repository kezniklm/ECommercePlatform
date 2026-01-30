using System.Globalization;
using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Errors;

public sealed class InvalidStockQuantityError : ValidationError
{
    public InvalidStockQuantityError(int quantity)
        : base(
            $"Stock quantity must be between 0 and 999,999,999. Provided: {quantity.ToString(CultureInfo.InvariantCulture)}")
    {
        Metadata["code"] = "product.invalid_stock_quantity";
        Metadata["quantity"] = quantity;
    }
}
