using FizzBuzz.DependencyInjection;
using FizzBuzz.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FizzBuzz.Api.Interop
{
    public class ServiceCollectionInterop : IServiceContainer
    {
        public ServiceCollectionInterop(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; }

        public IServiceContainer Add(Type serviceType, Type implementationType, Lifetime lifetime)
        {
            ServiceCollection.Add(new ServiceDescriptor(serviceType, implementationType, ConvertLifetime(lifetime)));

            return this;
        }

        public IServiceContainer Add(Type serviceType, Func<IServiceFactory, object> factory, Lifetime lifetime)
        {
            ServiceCollection.Add(new ServiceDescriptor(serviceType, provider =>
            {
                IServiceFactory serviceFactory = new ServiceProviderInterop(provider);
                return factory(serviceFactory);
            }, ConvertLifetime(lifetime)));

            return this;
        }

        private ServiceLifetime ConvertLifetime(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.Singleton:
                    return ServiceLifetime.Singleton;
                case Lifetime.Transient:
                    return ServiceLifetime.Transient;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
