using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace FizzBuzz.DependencyInjection.Helpers
{
    internal class InstanceFactory
    {
        private InstanceFactory()
        {
            _ctorArgsCache = new Dictionary<Type, (Delegate, Type[])>();
        }

        public static readonly InstanceFactory Instance = new InstanceFactory();

        private readonly IDictionary<Type, (Delegate, Type[])> _ctorArgsCache;

        public Lifetime Lifetime { get; }

        public (Delegate, Type[]) GetConstuctorArgs(Type typeToGetArgsFor)
        {
            (Delegate, Type[]) args;

            lock (_ctorArgsCache)
            {
                if (_ctorArgsCache.TryGetValue(typeToGetArgsFor, out args))
                {
                    return args;
                }

                args = GetMinimumConstructorParameters(typeToGetArgsFor);

                _ctorArgsCache.Add(typeToGetArgsFor, args);
            }

            return args;
        }

        public object CreateInstance(IServiceFactory serviceFactory, Type typeToCreate)
        {
            if (serviceFactory is null)
            {
                throw new ArgumentNullException(nameof(serviceFactory));
            }
            if (typeToCreate is null)
            {
                throw new ArgumentNullException(nameof(typeToCreate));
            }

            (Delegate ctor, Type[] ctorArgs) = GetConstuctorArgs(typeToCreate);

            var ctorArgsResolved = new object[ctorArgs.Length];

            for (var argIndex = 0; argIndex < ctorArgs.Length; argIndex++)
            {
                ctorArgsResolved[argIndex] = serviceFactory.Get(ctorArgs[argIndex]);
            }

            var instance = ctor.DynamicInvoke(ctorArgsResolved);

            return instance;
        }

        public object CreateInstance(IServiceFactory serviceFactory, Func<IServiceFactory, object> factory)
        {
            if (serviceFactory is null)
            {
                throw new ArgumentNullException(nameof(serviceFactory));
            }
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return factory(serviceFactory);
        }

        private (Delegate, Type[]) GetMinimumConstructorParameters(Type type)
        {
            if (!type.IsClass)
            {
                // Use default struct factory (0 parameters)
                var structExpression = Expression.Lambda(Expression.New(type)).Compile();

                return (structExpression, new Type[0]);
            }

            ConstructorInfo[] constructors = type.GetConstructors();
            Type[] minimumParams = null;
            ParameterExpression[] parametersAsExpression = null;
            ConstructorInfo minimumConstructor = null;

            foreach (ConstructorInfo ctor in constructors)
            {
                ParameterInfo[] ctorParams = ctor.GetParameters();

                if (minimumParams is null || minimumParams.Length > ctorParams.Length)
                {
                    // Convert to types
                    minimumConstructor = ctor;
                    minimumParams = new Type[ctorParams.Length];
                    parametersAsExpression = new ParameterExpression[ctorParams.Length];

                    for (var index = 0; index < ctorParams.Length; index++)
                    {
                        minimumParams[index] = ctorParams[index].ParameterType;
                        parametersAsExpression[index] = Expression.Parameter(ctorParams[index].ParameterType, ctorParams[index].Name);
                    }
                }
            }

            Delegate expression = Expression.Lambda(
                Expression.New(minimumConstructor, parametersAsExpression), 
                parametersAsExpression)
                .Compile();

            return (expression, minimumParams);
        }
    }
}
