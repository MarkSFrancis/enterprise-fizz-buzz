using FizzBuzz.DependencyInjection.Abstractions;
using System;

namespace FizzBuzz.Api.Interop
{
    public class ServiceProviderInterop : IServiceFactory
    {
        public ServiceProviderInterop(IServiceProvider provider)
        {
            Provider = provider;
        }

        public IServiceProvider Provider { get; }

        public object Get(Type type)
        {
            var resolved = Provider.GetService(type);

            return resolved;
        }
    }
}
