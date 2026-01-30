using ProductCatalog.Domain.Common;

namespace ProductCatalog.Domain.Product.Events;

public sealed record ProductCreatedEvent(Guid Id, Product Product) : DomainEvent(Id);
