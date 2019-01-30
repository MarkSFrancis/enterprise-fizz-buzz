using FizzBuzz.Console.Services;
using FizzBuzz.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FizzBuzz.Console
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceContainer container)
        {
            container.AddSingleton<IConsoleIo, ConsoleIo>();
            container.AddSingleton<IFizzBuzzConsole, FizzBuzzConsole>();
            container.AddSingleton<IFizzBuzzApp, FizzBuzzApp>();
        }

        public Task Run(IServiceFactory factory)
        {
            var app = factory.Get<IFizzBuzzApp>();

            return app.Run();
        }
    }
}
