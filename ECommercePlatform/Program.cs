using ECommercePlatform;
using ECommercePlatform.ServiceDefaults;
using ProductCatalog.Infrastructure.Persistence;
using Wolverine.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(nameof(ECommercePlatform));

builder.AddModules();

builder.ConfigureSwaggerDocuments();

builder.AddWolverineMessaging();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await SeedProductCatalogData.EnsureSeededAsync(app.Services, true);
}

app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapWolverineEndpoints();

app.UseSwaggerDocuments();

await app.RunAsync();
