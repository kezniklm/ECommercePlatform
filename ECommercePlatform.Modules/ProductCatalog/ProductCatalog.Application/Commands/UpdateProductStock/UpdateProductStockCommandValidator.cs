using FluentValidation;

namespace ProductCatalog.Application.Commands.UpdateProductStock;

public sealed class UpdateProductStockCommandValidator : AbstractValidator<UpdateProductStockCommand>
{
    public UpdateProductStockCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(x => x.NewStockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity must be greater than or equal to 0.")
            .LessThanOrEqualTo(999_999_999)
            .WithMessage("Stock quantity cannot exceed 999,999,999.");
    }
}
