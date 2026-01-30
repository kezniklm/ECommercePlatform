using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Services;

namespace ProductCatalog.Infrastructure.Persistence.Services;

public class EfCoreQueryObject<TAggregate>(ProductCatalogDbContext dbContext)
    : QueryObject<TAggregate>(dbContext.Set<TAggregate>().AsQueryable())
    where TAggregate : class
{
#pragma warning disable CA1823 // Avoid unused private fields
    private readonly ProductCatalogDbContext _dbContext = dbContext;
#pragma warning restore CA1823 // Avoid unused private fields

    public override async Task<IEnumerable<TAggregate>> ExecuteAsync() => await Query.ToListAsync();
}
