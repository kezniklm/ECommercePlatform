namespace ProductCatalog.Infrastructure.Persistence.Seeders.Fakers;

public interface IFaker<out T> where T : class
{
    IReadOnlyList<T> Generate(int count);
}
