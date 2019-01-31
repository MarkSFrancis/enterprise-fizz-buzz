namespace FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services
{
    public class MediumComplexityService : IMediumComplexityService
    {
        public MediumComplexityService(IBasicService basicService)
        {
            BasicService = basicService;
        }

        public IBasicService BasicService { get; }
    }
}
