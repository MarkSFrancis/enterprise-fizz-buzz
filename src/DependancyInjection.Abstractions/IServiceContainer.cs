using System;

namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IServiceContainer
    {
        IServiceContainer Add(Type serviceType, Type implementationType, Lifetime lifetime);
        IServiceContainer Add(Type serviceType, Func<IServiceFactory, object> factory, Lifetime lifetime);
    }
}