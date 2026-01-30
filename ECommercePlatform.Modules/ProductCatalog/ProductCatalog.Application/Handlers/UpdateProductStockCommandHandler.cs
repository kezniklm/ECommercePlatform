using FluentResults;
using ProductCatalog.Application.Commands.UpdateProductStock;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;
using ProductCatalog.Domain.Product.Errors;
using ProductCatalog.Domain.Product.ValueObjects;

namespace ProductCatalog.Application.Handlers;

public sealed class UpdateProductStockCommandHandler
{
    public static async Task<Result<UpdateProductStockCommand.Result>> Handle(
        UpdateProductStockCommand command,
        IQueryObject<Product> queryObject,
        IRepository<Product> repository)
    {
        var products = await queryObject
            .Filter(p => p.Id == ProductId.Create(command.ProductId))
            .ExecuteAsync();

        var product = products.FirstOrDefault();
        
        if (product is null)
        {
            return Result.Fail<UpdateProductStockCommand.Result>(new ProductNotFoundError(command.ProductId));
        }

        Result updateResult = product.UpdateStock(command.NewStockQuantity);

        if (updateResult.IsFailed)
        {
            return Result.Fail<UpdateProductStockCommand.Result>(updateResult.Errors);
        }

        repository.Update(product);

        await repository.CommitAsync();

        return new UpdateProductStockCommand.Result(product.Id.Value, product.ProductStockQuantity.Value);
    }
}
