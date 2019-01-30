using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IStartup : IDependancyInjectionSetup
    {
        Task Run(IServiceFactory factory);
    }
}
