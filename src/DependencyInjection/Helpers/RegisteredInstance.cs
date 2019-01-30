using FizzBuzz.DependencyInjection.Abstractions;
using System;

namespace FizzBuzz.DependencyInjection.Helpers
{
    internal class RegisteredType
    {
        public RegisteredType(Lifetime lifetime, Type implementation)
        {
            Lifetime = lifetime;
            ImplementationType = implementation;
        }

        public RegisteredType(Lifetime lifetime, Func<IServiceFactory, object> customConstructor)
        {
            Lifetime = lifetime;
            CustomConstructor = customConstructor;
        }

        public Lifetime Lifetime { get; }
        public Type ImplementationType { get; }
        public Func<IServiceFactory, object> CustomConstructor { get; }

        public bool HasCustomConstructor => CustomConstructor != null;
    }
}
