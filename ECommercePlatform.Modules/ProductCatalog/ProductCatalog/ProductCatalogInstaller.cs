using Microsoft.Extensions.DependencyInjection;
using ProductCatalog.Application;
using ProductCatalog.Domain;
using ProductCatalog.Infrastructure;
using ProductCatalog.Presentation;
using SharedKernel;

namespace ProductCatalog;

public class ProductCatalogInstaller : IModule
{
    public void InstallDomain(IServiceCollection serviceCollection) => DomainInstaller.Install(serviceCollection);

    public void InstallApplication(IServiceCollection serviceCollection) =>
        ApplicationInstaller.Install(serviceCollection);

    public void InstallInfrastructure(IServiceCollection serviceCollection, string connectionString) =>
        InfrastructureInstaller.Install(serviceCollection, connectionString);

    public void InstallPresentation(IServiceCollection serviceCollection) =>
        PresentationInstaller.Install(serviceCollection);
}
