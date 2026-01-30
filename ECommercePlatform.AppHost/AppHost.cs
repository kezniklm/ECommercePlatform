using Projects;

IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<SqlServerServerResource> sqlServer = builder
    .AddSqlServer(nameof(ECommercePlatform) + "SqlServer")
    .WithDataVolume();

IResourceBuilder<SqlServerDatabaseResource> productCatalogDb = sqlServer.AddDatabase("ProductCatalogDb");

builder.AddProject<ECommercePlatform>(nameof(ECommercePlatform))
    .WithUrl("/swagger")
    .WithReference(productCatalogDb, "ProductCatalogConnectionString")
    .WaitFor(sqlServer);

builder.Build().Run();
