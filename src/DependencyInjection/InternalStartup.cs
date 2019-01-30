using FizzBuzz.Services;

namespace FizzBuzz.DependencyInjection
{
    internal class InternalStartup : IDependancyInjectionSetup
    {
        public void AddServices(IServiceContainer container)
        {
            // Place any shared dependancies for FizzBuzz here
            container.AddSingleton<IFizzBuzzService, FizzBuzzService>();
            container.AddSingleton(_factory => _factory);
        }
    }
}
