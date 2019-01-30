using System;

namespace FizzBuzz.DependencyInjection.Abstractions
{
    public interface IServiceFactory
    {
        object Get(Type type);
    }
}
