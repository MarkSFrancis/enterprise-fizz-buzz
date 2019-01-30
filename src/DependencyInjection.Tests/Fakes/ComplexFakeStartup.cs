using FizzBuzz.Services;
using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection.Tests.Fakes
{
    public class ComplexFakeStartup : IStartup
    {
        public static bool AddedServices { get; private set; }
        public static bool RanToCompletion { get; private set; }
        public static IServiceFactory Factory { get; private set; }
        public static IFizzBuzzService FizzBuzzService { get; private set; }

        public ComplexFakeStartup(IServiceFactory factory, IFizzBuzzService fizzBuzzService)
        {
            Factory = factory;
            FizzBuzzService = fizzBuzzService;
        }

        public void AddServices(IServiceContainer container)
        {
            AddedServices = true;
        }

        public Task Run(IServiceFactory factory)
        {
            return Task.Delay(50).ContinueWith(t =>
            {
                RanToCompletion = true;
            });
        }

        public static void ResetState()
        {
            AddedServices = false;
            RanToCompletion = false;
            Factory = null;
            FizzBuzzService = null;
        }
    }
}
