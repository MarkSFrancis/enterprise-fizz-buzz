using System;

namespace FizzBuzz.DependencyInjection
{
    public interface IServiceFactory
    {
        object Get(Type type);
    }
}
