using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Domain.Product;
using ProductCatalog.Infrastructure.Persistence.Seeders;

namespace ProductCatalog.Infrastructure.Persistence;

public static class SeedProductCatalogData
{
    public static async Task EnsureSeededAsync(IServiceProvider services, bool resetDatabase = false)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();

        if (resetDatabase)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
        }

        var productSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeder<Product>>();

        await productSeeder.SeedAsync();
    }
}
