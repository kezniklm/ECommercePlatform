using ECommercePlatform;
using ECommercePlatform.ServiceDefaults;
using Wolverine.Http;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(nameof(ECommercePlatform));

builder.AddModules();

builder.ConfigureSwaggerDocuments();

builder.AddWolverineMessaging();

WebApplication app = builder.Build();

app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.MapWolverineEndpoints();

app.UseSwaggerDocuments();

app.Run();
