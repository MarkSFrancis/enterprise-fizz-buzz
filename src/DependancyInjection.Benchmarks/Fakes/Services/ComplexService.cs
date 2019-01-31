namespace FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services
{
    public class ComplexService : IComplexService
    {
        public ComplexService(IMediumComplexityService service)
        {
            Service = service;
        }

        public IMediumComplexityService Service { get; }
    }
}
