# Modular Monolith Template

A production-ready template for building .NET applications using the **Modular Monolith** architecture pattern with Clean Architecture principles.

## Overview

This template provides a solid foundation for building scalable, maintainable applications that can evolve from a monolith to microservices when needed. It combines the simplicity of a monolith with the modularity of microservices.

## Architecture

### Modular Monolith Pattern

Each module is self-contained with its own:
- **Domain Layer** - Business entities, value objects, and domain logic
- **Application Layer** - Use cases, services, and business rules
- **Infrastructure Layer** - Data persistence, external services integration
- **Presentation Layer** - API endpoints and DTOs

### Key Principles
- **ProductCatalog Independence** - Modules are isolated and communicate through well-defined interfaces
- **Clean Architecture** - Clear separation of concerns with dependency inversion
- **Shared Kernel** - Common abstractions and contracts shared across modules

## Technologies

- **.NET 10.0**
- **Aspire** - Cloud-ready stack for building observable, production-ready distributed applications
- **Wolverine** - Message bus and mediator for in-process and distributed messaging
- **Entity Framework Core** - Data access and persistence
- **OpenAPI/Swagger** - API documentation

## Project Structure

```
├── ECommercePlatform/                    # Main API host application
├── ECommercePlatform.AppHost/            # Aspire orchestration host
├── ECommercePlatform.ServiceDefaults/    # Shared service configuration (OpenTelemetry, Health Checks)
└── ECommercePlatform.Modules/
    ├── SharedKernel/             # Common interfaces and abstractions
    └── ProductCatalog/                   # Template module (copy this to create new modules)
        ├── ProductCatalog/               # ProductCatalog entry point and installer
        ├── ProductCatalog.Domain/        # Domain entities and logic
        ├── ProductCatalog.Application/   # Application services and use cases
        ├── ProductCatalog.Infrastructure/# Data access and external services
        └── ProductCatalog.Presentation/  # API endpoints
```

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- Docker (must be running to use Postgres in Aspire)
- Your preferred IDE (Visual Studio, VS Code, Rider)

### Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/kezniklm/Modular-Monolith-Template
   ```

2. **Build the solution**
   ```bash
   dotnet build
   ```

3. **Run with Aspire (recommended)**
   ```bash
   dotnet run --project ECommercePlatform.AppHost
   ```

4. **Or run standalone**
   ```bash
   dotnet run --project ECommercePlatform
   ```

## Creating a New ProductCatalog

1. Copy the `ECommercePlatform.Modules/ProductCatalog` folder
2. Rename all `ProductCatalog` references to your new module name
3. Register the module in `ECommercePlatform/DependencyInjection.cs`:
   ```csharp
   var modules = new List<IProductCatalog>
   {
       new ProductCatalogInstaller(),
       new YourNewProductCatalogInstaller() // Add your module here
   };
   ```
4. Add connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "YourNewProductCatalogConnectionString": "..."
     }
   }
   ```

## ProductCatalog Interface

Each module implements the `IProductCatalog` interface:

```csharp
public interface IProductCatalog
{
    void InstallDomain(IServiceCollection serviceCollection);
    void InstallApplication(IServiceCollection serviceCollection);
    void InstallInfrastructure(IServiceCollection serviceCollection, string connectionString);
    void InstallPresentation(IServiceCollection serviceCollection);
}
```

## Configuration

- `appsettings.json` - Application configuration
- `appsettings.Development.json` - Development-specific settings
- Connection strings follow the pattern: `{ProductCatalogName}ConnectionString`

## License

View [LICENSE.txt](LICENSE.txt) for licensing information.

## Contributing

Contributions are welcome! Please feel free to submit issues and pull requests.


