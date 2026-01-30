using Microsoft.Extensions.DependencyInjection;
using Wolverine.Attributes;

[assembly: WolverineModule]

namespace ProductCatalog.Domain;

public static class DomainInstaller
{
    public static IServiceCollection Install(IServiceCollection services) => services;
}
