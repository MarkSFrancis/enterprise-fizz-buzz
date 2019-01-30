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
            try
            {
                registeredType = _settings[type];
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

                instance = Resolve(registeredType);

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

        private object Resolve(RegisteredType registeredType)
        {
            if (registeredType.HasCustomConstructor)
            {
                var instance = _instanceFactory.CreateInstance(this, registeredType.CustomConstructor);

                return instance;
            }
            else
            {
                var instance = _instanceFactory.CreateInstance(this, registeredType.ImplementationType);

                return instance;
            }
        }
    }
}
