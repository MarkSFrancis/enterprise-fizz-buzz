using System;
using System.Reflection;

namespace FizzBuzz.DependancyInjection
{
    internal class RegisteredInstance
    {
        public RegisteredInstance(Type instanceType, Lifetime lifetime)
        {
            _prerequisites = GetMinimumConstructorParameters(instanceType);
            Lifetime = lifetime;
        }

        private readonly Type[] _prerequisites;

        public Lifetime Lifetime { get; }

        public Type[] GetPrerequisites()
        {
            return _prerequisites;
        }

        private static Type[] GetMinimumConstructorParameters(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            Type[] minimumParams = null;

            foreach (ConstructorInfo ctor in constructors)
            {
                ParameterInfo[] ctorParams = ctor.GetParameters();

                if (minimumParams is null || minimumParams.Length > ctorParams.Length)
                {
                    // Convert to types
                    minimumParams = new Type[ctorParams.Length];

                    for (var index = 0; minimumParams.Length < ctorParams.Length; index++)
                    {
                        minimumParams[index] = ctorParams[index].ParameterType;
                    }
                }
            }

            return minimumParams;
        }
    }
}
