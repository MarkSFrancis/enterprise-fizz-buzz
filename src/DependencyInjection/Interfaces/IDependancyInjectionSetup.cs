namespace FizzBuzz.DependencyInjection
{
    public interface IDependancyInjectionSetup
    {
        void AddServices(IServiceContainer container);
    }
}
