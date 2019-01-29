using System;

namespace FizzBuzz.DependancyInjection
{
    internal class TypeActivator
    {
        public object CreateInstance(Type typeToCreate, object[] ctorArgs)
        {
            return Activator.CreateInstance(typeToCreate, ctorArgs);
        }
    }
}
