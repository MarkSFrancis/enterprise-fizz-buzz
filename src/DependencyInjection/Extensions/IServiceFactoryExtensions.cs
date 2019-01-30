using System;

namespace FizzBuzz.DependencyInjection
{
    public static class IServiceFactoryExtensions
    {
        public static TService Get<TService>(this IServiceFactory serviceFactory)
        {
            if (serviceFactory is null)
            {
                throw new ArgumentNullException(nameof(serviceFactory));
            }

            var instance = serviceFactory.Get(typeof(TService));

            return (TService)instance;
        }
    }
}
