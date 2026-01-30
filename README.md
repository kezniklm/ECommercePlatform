# ECommercePlatform

## Overview
ECommercePlatform is a REST API service designed to manage products for an e-shop. It provides endpoints to list all products, create new products, retrieve product details, and update product stock. The application is built with .NET Core and follows best practices for REST API design, application architecture, and SOLID principles.

## Features
- **Product Management**:
  - List all available products.
  - Create new products with only a name and image URL.
  - Retrieve details of a single product by ID.
  - Update the stock quantity of a product.
- **API Documentation**:
  - Swagger documentation for all endpoints.
- **Unit Testing**:
  - Comprehensive unit tests covering endpoint functionality.
- **Versioning**:
  - Version 2 of the API includes pagination support (default page size: 10).
- **Asynchronous Queue**:
  - All endpoints use Wolverine for asynchronous processing, ensuring efficient and reliable handling of requests.

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
├── ECommercePlatform.Modules/
    ├── SharedKernel/             # Common interfaces and abstractions
    └── ProductCatalog/                   # Template module (copy this to create new modules)
        ├── ProductCatalog/               # ProductCatalog entry point and installer
        ├── ProductCatalog.Domain/        # Domain entities and logic
        ├── ProductCatalog.Application/   # Application services and use cases
        ├── ProductCatalog.Infrastructure/# Data access and external services
        └── ProductCatalog.Presentation/  # API endpoints
└── ECommercePlatform.Modules.Tests/      # Unit tests for modules
    ├── ProductCatalog.Tests/             # Tests for ProductCatalog module
        ├── ProductCatalog.Application/   # Application layer tests
        ├── ProductCatalog.Domain/        # Domain layer tests
        ├── ProductCatalog.Infrastructure/# Infrastructure layer tests
        └── ProductCatalog.Presentation/  # Presentation layer tests
```

## Getting Started

### Prerequisites
- .NET 10.0 SDK
- Docker (must be running to use MS SQLServer in Aspire)
- Your preferred IDE (Visual Studio, VS Code, Rider)

### 1. Clone the Repository
```bash
git clone https://github.com/kezniklm/ECommercePlatform
cd ECommercePlatform
```

### 2. Run the Application
```bash
dotnet run --project ECommercePlatform.AppHost
```
The API will be accessible at `https://localhost:17162/`.

### 4. Access Swagger Documentation
Open your browser and navigate to `https://localhost:7079/swagger` to view the API documentation.

### 5. Run Unit Tests
```bash
dotnet test
```

## Project Structure
- **ECommercePlatform**: Main Web API project.
- **ECommercePlatform.AppHost**: Application host configuration.
- **ECommercePlatform.Modules**: Modular structure for features like ProductCatalog.
- **ECommercePlatform.Modules.Tests**: Unit tests for the modules.
- **ECommercePlatform.ServiceDefaults**: Shared service configurations.
- **SharedKernel**: Common interfaces and utilities.

## Version 2 Enhancements
- **Pagination**:
  - Default page size: 10.
  - Customizable via query parameters.
- **Asynchronous Stock Updates**:
  - Uses an InMemory queue, Kafka, or RabbitMQ for handling stock updates asynchronously.

## Architecture
The application follows a modular architecture with clear separation of concerns:
- **Domain Layer**: Core business logic.
- **Application Layer**: Application-specific logic and use cases.
- **Infrastructure Layer**: Database and external service integrations.
- **Presentation Layer**: API controllers and request handling.

## License
View [LICENSE.txt](LICENSE.txt) for licensing information.
