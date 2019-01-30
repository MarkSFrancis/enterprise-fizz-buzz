using FizzBuzz.DependencyInjection.Abstractions;
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

        public IServiceContainer Add(Type serviceType, Type implementationType, Lifetime lifetime)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (implementationType is null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            if (!serviceType.ContainsGenericParameters && !serviceType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException($"{nameof(implementationType)} is not assignable from {nameof(serviceType)}");
            }

            var settings = new RegisteredType(lifetime, implementationType);

            UpdateContainerEntry(serviceType, settings);

            return this;
        }

        public IServiceContainer Add(Type serviceType, Func<IServiceFactory, object> factory, Lifetime lifetime)
        {
            if (serviceType is null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var settings = new RegisteredType(lifetime, factory);

            UpdateContainerEntry(serviceType, settings);

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
