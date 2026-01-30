using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Domain.Product;
using ProductCatalog.Domain.Product.ValueObjects;

namespace ProductCatalog.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(nameof(Product) + 's');

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value))
            .ValueGeneratedNever();

        builder.Property(p => p.Name)
            .HasConversion(
                name => name.Value,
                value => ProductName.Create(value).Value)
            .HasMaxLength(ProductName.MaxLength)
            .IsRequired();

        builder.Property(p => p.ProductImageUrl)
            .HasConversion(
                url => url.Value.ToString(),
                value => ProductImageUrl.Create(new Uri(value)).Value)
            .HasMaxLength(ProductImageUrl.MaxLength)
            .IsRequired();

        builder.Property(p => p.Price)
            .HasConversion(
                price => price.Amount,
                value => ProductPrice.Create(value).Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(p => p.ProductDescription)
            .HasConversion(
                desc => desc.Value,
                value => ProductDescription.Create(value).Value)
            .HasMaxLength(ProductDescription.MaxLength)
            .IsRequired(false);

        builder.Property(p => p.ProductStockQuantity)
            .HasConversion(
                stock => stock.Value,
                value => ProductStockQuantity.Create(value).Value)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Ignore(p => p.IsAvailable);
        builder.Ignore(p => p.DomainEvents);
    }
}
