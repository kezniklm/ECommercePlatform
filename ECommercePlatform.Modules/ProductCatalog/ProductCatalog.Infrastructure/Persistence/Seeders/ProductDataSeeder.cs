using ProductCatalog.Domain.Product;
using ProductCatalog.Infrastructure.Persistence.Seeders.Fakers;

namespace ProductCatalog.Infrastructure.Persistence.Seeders;

internal sealed class ProductDataSeeder(
    ProductCatalogDbContext context,
    IFaker<Product> productFaker)
    : IDataSeeder<Product>
{
    private readonly ProductCatalogDbContext _context = context;
    private readonly IFaker<Product> _productFaker = productFaker;

    public async Task SeedAsync(CancellationToken ct = default)
    {
        if (_context.Products.Any())
        {
            return;
        }

        var products = _productFaker.Generate(200);

        await _context.Products.AddRangeAsync(products, ct);

        await _context.SaveChangesAsync(ct);
    }
}

