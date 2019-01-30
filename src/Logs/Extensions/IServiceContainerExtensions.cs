﻿using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.Logs.Outputs;
using Logs.Outputs;
using System;

namespace FizzBuzz.Logs.Extensions
{
    public static class IServiceContainerExtensions
    {
        public static IServiceContainer AddLogging(this IServiceContainer serviceContainer, LogLevel minimumLogLevel = LogLevel.Info)
        {
            serviceContainer.AddLogging((container, setup) =>
            {
                setup.AddOutput<DebugLog>();
                setup.AddOutput<ConsoleLog>();
                setup.SetMinimumLogLevel(minimumLogLevel);
            });

            return serviceContainer;
        }

        public static IServiceContainer AddLogging(this IServiceContainer serviceContainer, Action<IServiceFactory, ILoggerSetup> configuration)
        {
            serviceContainer.AddSingleton<LoggerFactory, LoggerFactory>(serviceFactory =>
            {
                var loggerFactory = new LoggerFactory(serviceContainer);
                configuration(serviceFactory, loggerFactory);

                return loggerFactory;
            });

            serviceContainer.AddSingleton<ILoggerFactory, LoggerFactory>(serviceFactory => serviceFactory.Get<LoggerFactory>());
            serviceContainer.AddSingleton<ILoggerSetup, LoggerFactory>(serviceFactory => serviceFactory.Get<LoggerFactory>());
            serviceContainer.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            serviceContainer.AddTransient<ILogger, Logger>();

            return serviceContainer;
        }
    }
}
