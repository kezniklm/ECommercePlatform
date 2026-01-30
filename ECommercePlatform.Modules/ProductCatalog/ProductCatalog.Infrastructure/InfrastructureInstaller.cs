using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application.Services;
using ProductCatalog.Domain.Product;
using ProductCatalog.Infrastructure.Persistence;
using ProductCatalog.Infrastructure.Persistence.Seeders;
using ProductCatalog.Infrastructure.Persistence.Seeders.Fakers;
using Wolverine.Attributes;

[assembly: WolverineModule]

namespace ProductCatalog.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection Install(IServiceCollection serviceCollection, string moduleConnectionString)
    {
        RegisterQueryObjects(serviceCollection);

        RegisterRepositories(serviceCollection);

        RegisterSeeders(serviceCollection);

        serviceCollection.AddDbContext<ProductCatalogDbContext>(options => options.UseSqlServer(moduleConnectionString));

        serviceCollection.AddScoped<IDataSeeder<Product>, ProductDataSeeder>();

        return serviceCollection;
    }

    private static void RegisterQueryObjects(IServiceCollection serviceCollection) =>
        serviceCollection.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureInstaller).Assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(IQueryObject<>)), false)
            .AsImplementedInterfaces()
            .WithTransientLifetime());

    private static void RegisterRepositories(IServiceCollection serviceCollection) =>
        serviceCollection.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureInstaller).Assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(IRepository<>)), false)
            .AsImplementedInterfaces()
            .WithTransientLifetime());

    private static void RegisterSeeders(IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureInstaller).Assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(IDataSeeder<>)), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        serviceCollection.Scan(scan => scan
            .FromAssemblies(typeof(InfrastructureInstaller).Assembly)
            .AddClasses(filter => filter.AssignableTo(typeof(IFaker<>)), false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
