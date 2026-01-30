using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product;
using Wolverine;

namespace ProductCatalog.Infrastructure.Persistence;

public class ProductCatalogDbContext(
    DbContextOptions<ProductCatalogDbContext> options,
    IMessageBus sender,
    ILogger<ProductCatalogDbContext> logger)
    : DbContext(options)
{
    private readonly ILogger<ProductCatalogDbContext> _logger = logger;

    private readonly IMessageBus _sender = sender;

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        IEnumerable<DomainEvent> domainEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(entityEntry => entityEntry.Entity)
            .Where(baseEntity => baseEntity.DomainEvents.Count != 0)
            .SelectMany(baseEntity => baseEntity.DomainEvents);

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (DomainEvent domainEvent in domainEvents)
        {
            await _sender.PublishAsync(domainEvent);

            try
            {
                await _sender.PublishAsync(domainEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error publishing domain event {EventType}. Event: {@Event}",
                    domainEvent.GetType().Name,
                    domainEvent
                );
            }
        }

        return result;
    }
}
