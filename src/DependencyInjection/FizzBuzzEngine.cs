using FizzBuzz.DependencyInjection.Helpers;
using System;

namespace FizzBuzz.DependencyInjection
{
    public class FizzBuzzEngine<TStartup> where TStartup : IStartup
    {
        private readonly ServiceContainer _serviceContainer;
        private readonly IDependancyInjectionSetup _internalStartup;
        private readonly TStartup _startupInstance;
        private IStartup appStartup;

        public FizzBuzzEngine()
        {
            _serviceContainer = new ServiceContainer();
            _internalStartup = new InternalStartup();
        }

        public FizzBuzzEngine(TStartup startupInstance)
        {
            _serviceContainer = new ServiceContainer();
            _internalStartup = new InternalStartup();
            _startupInstance = startupInstance;
        }

        public void Build()
        {
            _internalStartup.AddServices(_serviceContainer);

            if (_startupInstance != null)
            {
                // Startup is already built
                _serviceContainer.AddTransient(factory => _startupInstance);
                appStartup = _startupInstance;
            }
            else
            {
                _serviceContainer.AddTransient<TStartup>();

                var temporaryFactory = GetFactory();

                appStartup = temporaryFactory.Get<TStartup>();
            }

            appStartup.AddServices(_serviceContainer);
        }

        public void Run()
        {
            if (appStartup is null)
            {
                throw new InvalidOperationException($"The {nameof(FizzBuzzEngine<TStartup>)} must be built with .{nameof(Build)}() before it can be run");
            }

            var factory = GetFactory();
            var task = appStartup.Run(factory);

            task.Wait();
        }

        private IServiceFactory GetFactory()
        {
            var settings = _serviceContainer.ExportSettings();

            var factory = new ServiceFactory(InstanceFactory.Instance, settings);

            return factory;
        }
    }
}
