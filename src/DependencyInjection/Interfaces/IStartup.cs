using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection
{
    public interface IStartup : IDependancyInjectionSetup
    {
        Task Run(IServiceFactory factory);
    }
}
