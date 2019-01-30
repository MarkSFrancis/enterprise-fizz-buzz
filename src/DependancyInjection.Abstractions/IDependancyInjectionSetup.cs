namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IDependancyInjectionSetup
    {
        void AddServices(IServiceContainer container);
    }
}
