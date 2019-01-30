using System;

namespace FizzBuzz.DependencyInjection.Abstractions
{
    public static class IServiceContainerExtensions
    {
        public static IServiceContainer Add(this IServiceContainer container, Type type, Lifetime lifetime)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.Add(type, type, lifetime);
        }

        public static IServiceContainer Add<T>(this IServiceContainer container, Lifetime lifetime)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.Add(typeof(T), typeof(T), lifetime);
        }

        public static IServiceContainer Add<TService, TImplementation>(this IServiceContainer container, Lifetime lifetime)
            where TImplementation : TService
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.Add(typeof(TService), typeof(TImplementation), lifetime);
        }

        public static IServiceContainer Add<T>(this IServiceContainer container, Func<IServiceFactory, T> factory, Lifetime lifetime)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return container.Add(typeof(T), serviceFactory => factory(serviceFactory), lifetime);
        }

        public static IServiceContainer Add<TService, TImplementation>(this IServiceContainer container, Func<IServiceFactory, TImplementation> factory, Lifetime lifetime)
            where TImplementation : TService
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return container.Add(typeof(TService), serviceFactory => factory(serviceFactory), lifetime);
        }

        public static IServiceContainer AddTransient<T>(this IServiceContainer container)
        {
            return Add<T>(container, Lifetime.Transient);
        }

        public static IServiceContainer AddTransient(this IServiceContainer container, Type serviceType)
        {
            return Add(container, serviceType, Lifetime.Transient);
        }

        public static IServiceContainer AddTransient(this IServiceContainer container, Type serviceType, Type implementationType)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.Add(serviceType, implementationType, Lifetime.Transient);
        }

        public static IServiceContainer AddTransient<TService, TImplementation>(this IServiceContainer container)
            where TImplementation : TService
        {
            return Add<TService, TImplementation>(container, Lifetime.Transient);
        }

        public static IServiceContainer AddTransient<T>(this IServiceContainer container, Func<IServiceFactory, T> factory)
        {
            return Add(container, factory, Lifetime.Transient);
        }

        public static IServiceContainer AddTransient<TService, TImplementation>(this IServiceContainer container, Func<IServiceFactory, TImplementation> factory)
            where TImplementation : TService
        {
            return Add<TService, TImplementation>(container, factory, Lifetime.Transient);
        }

        public static IServiceContainer AddSingleton<T>(this IServiceContainer container)
        {
            return Add<T>(container, Lifetime.Singleton);
        }

        public static IServiceContainer AddSingleton<T>(this IServiceContainer container, Type serviceType)
        {
            return Add(container, serviceType, Lifetime.Singleton);
        }

        public static IServiceContainer AddSingleton<T>(this IServiceContainer container, Type serviceType, Type implementationType)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            return container.Add(serviceType, implementationType, Lifetime.Singleton);
        }

        public static IServiceContainer AddSingleton<TService, TImplementation>(this IServiceContainer container)
            where TImplementation : TService
        {
            return Add<TService, TImplementation>(container, Lifetime.Singleton);
        }

        public static IServiceContainer AddSingleton<T>(this IServiceContainer container, Func<IServiceFactory, T> factory)
        {
            return Add<T>(container, factory, Lifetime.Singleton);
        }

        public static IServiceContainer AddSingleton<TService, TImplementation>(this IServiceContainer container, Func<IServiceFactory, TImplementation> factory)
            where TImplementation : TService
        {
            return Add<TService, TImplementation>(container, factory, Lifetime.Singleton);
        }
    }
}
