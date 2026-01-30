using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresServerResource> postgres =
    builder.AddPostgres(nameof(ECommercePlatform) + "Postgres").WithDataVolume();

IResourceBuilder<PostgresDatabaseResource> productCatalogDb = postgres.AddDatabase("ProductCatalogDb");

builder.AddProject<ECommercePlatform>(nameof(ECommercePlatform))
    .WithUrl("/swagger")
    .WithReference(productCatalogDb, "ProductCatalogConnectionString")
    .WaitFor(postgres);

builder.Build().Run();
