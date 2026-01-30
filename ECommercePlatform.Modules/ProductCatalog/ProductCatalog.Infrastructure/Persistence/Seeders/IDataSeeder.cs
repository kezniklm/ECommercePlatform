namespace ProductCatalog.Infrastructure.Persistence.Seeders;

internal interface IDataSeeder<TAggregate> where TAggregate : class
{
    Task SeedAsync(CancellationToken ct = default);
}
