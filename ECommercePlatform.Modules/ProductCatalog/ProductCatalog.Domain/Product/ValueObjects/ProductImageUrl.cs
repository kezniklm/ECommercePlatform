using FluentResults;
using ProductCatalog.Domain.Common;
using ProductCatalog.Domain.Product.Errors;

namespace ProductCatalog.Domain.Product.ValueObjects;

public sealed class ProductImageUrl : ValueObject
{
    public const int MaxLength = 2048;

    private static readonly string[] AllowedSchemes = ["http", "https"];

    private static readonly string[] AllowedExtensions = [".jpg", ".jpeg", ".png", ".webp", ".svg", ".bmp"];

    private ProductImageUrl(Uri value) => Value = value;

    public Uri Value { get; }

    public static Result<ProductImageUrl> Create(Uri url)
    {
        ArgumentNullException.ThrowIfNull(url);

        var urlString = url.ToString();

        if (urlString.Length > MaxLength)
        {
            return new InvalidImageUrlError(url, $"URL cannot exceed {MaxLength} characters.");
        }

        if (!url.IsAbsoluteUri)
        {
            return new InvalidImageUrlError(url, "URL must be an absolute URL.");
        }

        if (!AllowedSchemes.Contains(url.Scheme.ToLowerInvariant()))
        {
            return new InvalidImageUrlError(url, "URL must use HTTP or HTTPS protocol.");
        }

        var extension = Path.GetExtension(url.AbsolutePath).ToLowerInvariant();

        if (!string.IsNullOrEmpty(extension) && !AllowedExtensions.Contains(extension))
        {
            return new InvalidImageUrlError(url,
                $"URL must point to a valid image file ({string.Join(", ", AllowedExtensions)}).");
        }

        return new ProductImageUrl(url);
    }

    public static Result<ProductImageUrl> Create(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return new InvalidImageUrlError("URL cannot be empty.");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
        {
            return new InvalidImageUrlError("URL must be a valid absolute URL.");
        }

        return Create(uri);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
