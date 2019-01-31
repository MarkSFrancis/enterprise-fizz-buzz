namespace FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services
{
    public interface IComplexService
    {
        IMediumComplexityService Service { get; }
    }
}