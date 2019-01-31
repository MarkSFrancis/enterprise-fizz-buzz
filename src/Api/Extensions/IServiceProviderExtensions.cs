using FizzBuzz.Api.Interop;
using FizzBuzz.Api.Logs;
using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.Logs;
using FizzBuzz.Logs.Outputs;
using FizzBuzz.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace FizzBuzz.Api
{
    public static class IServiceProviderExtensions
    {
        public static IServiceCollection AddFizzBuzz(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(serviceCollection);

            serviceCollection.AddSingleton<IServiceContainer, ServiceCollectionInterop>();
            serviceCollection.AddTransient<IServiceFactory, ServiceProviderInterop>();
            serviceCollection.AddSingleton<IFizzBuzzService, FizzBuzzService>();

            return serviceCollection;
        }

        public static IServiceCollection AddFizzBuzzLogging(this IServiceCollection serviceCollection, FizzBuzz.Logs.LogLevel minimumLogLevel = FizzBuzz.Logs.LogLevel.Info)
        {
            serviceCollection.AddFizzBuzzLogging(setup =>
            {
                setup.AddOutput<DebugLog>();
                setup.AddOutput<ConsoleLog>();
                setup.SetMinimumLogLevel(minimumLogLevel);
            });

            return serviceCollection;
        }

        public static IServiceCollection AddFizzBuzzLogging(this IServiceCollection serviceCollection, Action<ILoggerSetup> configuration)
        {
            var serviceContainer = new ServiceCollectionInterop(serviceCollection);

            serviceContainer.AddLogging(configuration);

            return serviceCollection;
        }
    }
}
