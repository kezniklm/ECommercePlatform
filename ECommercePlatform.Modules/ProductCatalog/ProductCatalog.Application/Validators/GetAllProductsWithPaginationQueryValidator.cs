using FluentValidation;
using ProductCatalog.Application.Queries.GetAllProducts;

namespace ProductCatalog.Application.Validators;

public class GetAllProductsWithPaginationQueryValidator : AbstractValidator<GetAllProductsWithPaginationQuery>
{
    public GetAllProductsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
    }
}
