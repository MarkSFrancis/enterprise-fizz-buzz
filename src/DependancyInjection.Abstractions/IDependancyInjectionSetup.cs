namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IDependencyInjectionSetup
    {
        void AddServices(IServiceContainer container);
    }
}
