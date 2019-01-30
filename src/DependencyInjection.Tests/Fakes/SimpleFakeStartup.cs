using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection.Tests.Fakes
{
    public class SimpleFakeStartup : IStartup
    {
        public static bool AddedServices { get; private set; }
        public static bool RanToCompletion { get; private set; }

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
        }
    }
}
