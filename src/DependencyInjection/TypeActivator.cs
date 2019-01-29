using System;

namespace FizzBuzz.DependencyInjection
{
    internal class TypeActivator
    {
        public object CreateInstance(Type typeToCreate, object[] ctorArgs)
        {
            return Activator.CreateInstance(typeToCreate, ctorArgs);
        }
    }
}
