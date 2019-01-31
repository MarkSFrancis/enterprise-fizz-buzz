using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IStartup : IDependencyInjectionSetup
    {
        Task Run(IServiceFactory factory);
    }
}
