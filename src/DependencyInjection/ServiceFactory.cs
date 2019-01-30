using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.DependencyInjection.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FizzBuzz.DependencyInjection
{
    internal class ServiceFactory : IServiceFactory
    {
        public ServiceFactory(InstanceFactory instanceFactory, IDictionary<Type, RegisteredType> settings)
        {
            _instanceFactory = instanceFactory ?? throw new ArgumentNullException(nameof(instanceFactory));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _singletons = new Dictionary<Type, object>();
        }

        private readonly InstanceFactory _instanceFactory;
        private readonly IDictionary<Type, RegisteredType> _settings;
        private readonly IDictionary<Type, object> _singletons;

        public object Get(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            RegisteredType registeredType;
            Type[] genericTypeArguments;

            try
            {
                if (type.GenericTypeArguments.Length > 0)
                {
                    if (!_settings.TryGetValue(type, out registeredType))
                    {
                        registeredType = _settings[type.GetGenericTypeDefinition()];
                    }
                    genericTypeArguments = type.GenericTypeArguments;
                }
                else
                {
                    registeredType = _settings[type];
                    genericTypeArguments = new Type[0];
                }
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"The type {type.FullName} is not registered in the container, and could not be resolved", ex);
            }

            object instance;
            try
            {
                if (registeredType.Lifetime == Lifetime.Singleton)
                {
                    // Lock all singletons to prevent duplicate singletons being created. Transients do not require the lock
                    Monitor.Enter(_singletons);

                    // Check if already created
                    if (_singletons.TryGetValue(type, out instance))
                    {
                        return instance;
                    }
                }

                instance = Resolve(registeredType, genericTypeArguments);

                if (registeredType.Lifetime == Lifetime.Singleton)
                {
                    _singletons.Add(type, instance);
                }

                return instance;
            }
            finally
            {
                if (registeredType.Lifetime == Lifetime.Singleton)
                {
                    // Exit singletons lock
                    Monitor.Exit(_singletons);
                }
            }
        }

        private object Resolve(RegisteredType registeredType, Type[] genericTypeArguments)
        {
            if (registeredType.HasCustomConstructor)
            {
                var instance = _instanceFactory.CreateInstance(this, registeredType.CustomConstructor);

                return instance;
            }
            else
            {
                Type implementation = registeredType.ImplementationType;
                if (genericTypeArguments.Length > 0)
                {
                    implementation = implementation.MakeGenericType(genericTypeArguments);
                }

                var instance = _instanceFactory.CreateInstance(this, implementation);

                return instance;
            }
        }
    }
}
