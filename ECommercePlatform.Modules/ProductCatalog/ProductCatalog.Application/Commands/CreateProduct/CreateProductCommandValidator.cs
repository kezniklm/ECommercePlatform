using FluentValidation;

namespace ProductCatalog.Application.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product)
            .NotNull()
            .WithMessage("Product data is required.");

        RuleFor(x => x.Product.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(x => x.Product.ImageUrl)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Product image URL is required.");
    }
}
