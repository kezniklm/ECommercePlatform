using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Events;

public sealed record ProductStockUpdatedEvent(
    Guid Id,
    Guid ProductId,
    int PreviousQuantity,
    int NewQuantity,
    DateTime UpdatedAt) : DomainEvent(Id)
{
    public static ProductStockUpdatedEvent Create(Guid productId, int previousQuantity, int newQuantity) =>
        new(Guid.NewGuid(), productId, previousQuantity, newQuantity, DateTime.UtcNow);
}
