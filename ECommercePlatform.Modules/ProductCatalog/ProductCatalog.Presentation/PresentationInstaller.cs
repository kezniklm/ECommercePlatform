using Microsoft.Extensions.DependencyInjection;
using Wolverine.Attributes;

[assembly: WolverineModule]

namespace ProductCatalog.Presentation;

public static class PresentationInstaller
{
    public static IServiceCollection Install(IServiceCollection serviceCollection) => serviceCollection;
}
