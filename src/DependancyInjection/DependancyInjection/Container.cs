using System;

namespace FizzBuzz.DependancyInjection
{
    public class Container
    {
        internal TypeActivator TypeActivator { get; }

        public Container()
        {
            TypeActivator = new TypeActivator();
        }

        public void AddTransient<T>()
        {
            throw new NotImplementedException();
        }

        public void AddSingleton<T>()
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>()
        {
            return (T)TypeActivator.CreateInstance(typeof(T), new object[0]);
        }
    }
}
