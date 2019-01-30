using System;

namespace FizzBuzz.DependencyInjection
{
    public interface IServiceContainer
    {
        IServiceContainer Add(Type service, Type implementation, Lifetime lifetime);
        IServiceContainer Add(Type service, Func<IServiceFactory, object> factory, Lifetime lifetime);
    }
}