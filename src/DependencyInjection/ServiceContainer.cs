using FizzBuzz.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;

namespace FizzBuzz.DependencyInjection
{
    internal class ServiceContainer : IServiceContainer
    {
        public ServiceContainer()
        {
            _settings = new Dictionary<Type, RegisteredType>();
        }

        private readonly IDictionary<Type, RegisteredType> _settings;

        public IServiceContainer Add(Type service, Type implementation, Lifetime lifetime)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }
            if (!service.IsAssignableFrom(implementation))
            {
                throw new ArgumentException($"{nameof(implementation)} is not assignable from {nameof(service)}");
            }

            var settings = new RegisteredType(lifetime, implementation);

            UpdateContainerEntry(service, settings);

            return this;
        }

        public IServiceContainer Add(Type service, Func<IServiceFactory, object> factory, Lifetime lifetime)
        {
            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var settings = new RegisteredType(lifetime, factory);

            UpdateContainerEntry(service, settings);

            return this;
        }

        internal IDictionary<Type, RegisteredType> ExportSettings()
        {
            return _settings;
        }

        private void UpdateContainerEntry(Type type, RegisteredType entrySettings)
        {
            lock (_settings)
            {
                if (_settings.ContainsKey(type))
                {
                    // Replace
                    _settings[type] = entrySettings;
                }
                else
                {
                    // Add
                    _settings.Add(type, entrySettings);
                }
            }
        }
    }
}
