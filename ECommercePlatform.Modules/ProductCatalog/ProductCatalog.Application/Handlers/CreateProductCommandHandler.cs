using FluentResults;
using ProductCatalog.Application.Commands.CreateProduct;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;

namespace ProductCatalog.Application.Handlers;

public sealed class CreateProductCommandHandler
{
    public static async Task<Result<CreateProductCommand.Result>> Handle(CreateProductCommand command,
        IRepository<Product> repository)
    {
        Result<Product> productResult = Product.Create(command.Product.Name, command.Product.ImageUrl);

        if (productResult.IsFailed)
        {
            return Result.Fail<CreateProductCommand.Result>(productResult.Errors);
        }

        await repository.InsertAsync(productResult.Value);

        await repository.CommitAsync();

        return new CreateProductCommand.Result(productResult.Value.Id.Value);
    }
}
