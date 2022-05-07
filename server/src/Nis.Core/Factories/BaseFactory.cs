using Bogus;

namespace Nis.Core.Factories;

public abstract class BaseFactory<TProduct> where TProduct : class
{
    protected readonly Faker<TProduct> Faker;

    protected abstract Faker<TProduct> Rules { get; }

    protected BaseFactory() => Faker = new Faker<TProduct>(locale: "cz");

    public abstract TProduct[] Create(short count = 1);
}
