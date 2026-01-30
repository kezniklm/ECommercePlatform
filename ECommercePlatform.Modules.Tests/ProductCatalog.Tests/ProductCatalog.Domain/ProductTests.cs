using System.Diagnostics.CodeAnalysis;
using AwesomeAssertions;
using FluentResults;
using ProductCatalog.Infrastructure.Persistence.Seeders.Fakers;
using Xunit;

namespace ProductCatalog.Domain.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores",
    Justification = "This is a test assembly")]
public class ProductTests
{
    private readonly IFaker<Product.Product> _faker = new ProductFaker();

    [Fact]
    public void Create_ValidProduct_Succeeds()
    {
        // Arrange
        const string name = "Test Product";
        const string imageUrl = "https://example.com/image.jpg";

        // Act
        Result<Product.Product> result = Product.Product.Create(name, imageUrl);

        // Assert
        result.IsSuccess.Should().BeTrue();
        Product.Product? product = result.Value;
        product.Name.Value.Should().Be(name);
        product.ProductImageUrl.Value.ToString().Should().Be(imageUrl);
        product.Price.Amount.Should().Be(0);
        product.ProductDescription.Value.Should().BeNull();
        product.ProductStockQuantity.Value.Should().Be(0);
    }

    [Fact]
    public void Create_InvalidName_Fails()
    {
        // Arrange
        const string imageUrl = "https://example.com/image.jpg";

        // Act
        Result<Product.Product> result = Product.Product.Create("", imageUrl);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Create_WithNullImageUrl_Fails()
    {
        // Arrange
        const string name = "Test Product";
        string? imageUrl = null;

        // Act
        Result<Product.Product> result = Product.Product.Create(name, imageUrl!);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UpdateName_Valid_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        const string newName = "New Name";

        // Act
        Result result = product.UpdateName(newName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.Name.Value.Should().Be(newName);
    }

    [Fact]
    public void UpdateName_Invalid_Fails()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];

        // Act
        Result result = product.UpdateName("");

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UpdateName_ToSameValue_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var originalName = product.Name.Value;

        // Act
        Result result = product.UpdateName(originalName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.Name.Value.Should().Be(originalName);
    }

    [Fact]
    public void UpdateImageUrl_Valid_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var newUrl = new Uri("https://example.com/new.jpg");

        // Act
        Result result = product.UpdateImageUrl(newUrl);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductImageUrl.Value.ToString().Should().Be(newUrl.ToString());
    }

    [Fact]
    public void UpdateImageUrl_Invalid_ThrowsArgumentNullException()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];

        // Act
        Action act = () => product.UpdateImageUrl(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void UpdateImageUrl_ToSameValue_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        Uri originalUrl = product.ProductImageUrl.Value;

        // Act
        Result result = product.UpdateImageUrl(originalUrl);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductImageUrl.Value.Should().Be(originalUrl);
    }

    [Fact]
    public void UpdatePrice_Valid_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        const decimal newPrice = 99.99m;

        // Act
        Result result = product.UpdatePrice(newPrice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.Price.Amount.Should().Be(newPrice);
    }

    [Fact]
    public void UpdatePrice_Invalid_Fails()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];

        // Act
        Result result = product.UpdatePrice(-1);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UpdatePrice_ToSameValue_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var originalPrice = product.Price.Amount;

        // Act
        Result result = product.UpdatePrice(originalPrice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.Price.Amount.Should().Be(originalPrice);
    }

    [Fact]
    public void UpdateDescription_Valid_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        const string newDescription = "A new description";

        // Act
        Result result = product.UpdateDescription(newDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductDescription.Value.Should().Be(newDescription);
    }

    [Fact]
    public void UpdateDescription_Invalid_Fails()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var tooLongDescription = new string('a', 4001);

        // Act
        Result result = product.UpdateDescription(tooLongDescription);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UpdateDescription_ToSameValue_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var originalDescription = product.ProductDescription.Value;

        // Act
        Result result = product.UpdateDescription(originalDescription);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductDescription.Value.Should().Be(originalDescription);
    }

    [Fact]
    public void UpdateDescription_ToNull_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];

        // Act
        Result result = product.UpdateDescription(null);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void UpdateStock_Valid_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        const int newStock = 10;

        // Act
        Result result = product.UpdateStock(newStock);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductStockQuantity.Value.Should().Be(newStock);
    }

    [Fact]
    public void UpdateStock_Invalid_Fails()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        const int invalidStock = -5;

        // Act
        Result result = product.UpdateStock(invalidStock);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void UpdateStock_ToSameValue_Succeeds()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];
        var originalStock = product.ProductStockQuantity.Value;

        // Act
        Result result = product.UpdateStock(originalStock);

        // Assert
        result.IsSuccess.Should().BeTrue();
        product.ProductStockQuantity.Value.Should().Be(originalStock);
    }

    [Fact]
    public void UpdateStock_ToNegative_Fails()
    {
        // Arrange
        Product.Product product = _faker.Generate(1)[0];

        // Act
        Result result = product.UpdateStock(-1);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Create_WithLongName_Fails()
    {
        // Arrange
        var longName = new string('a', 201);
        const string imageUrl = "https://example.com/image.jpg";

        // Act
        Result<Product.Product> result = Product.Product.Create(longName, imageUrl);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Create_WithInvalidImageUrl_Fails()
    {
        // Arrange
        const string name = "Test Product";
        const string imageUrl = "not-a-valid-url";

        // Act
        Result<Product.Product> result = Product.Product.Create(name, imageUrl);

        // Assert
        result.IsFailed.Should().BeTrue();
    }
}
