using FizzBuzz.DependencyInjection;

namespace FizzBuzz.Logs
{
    public class Logger<TSource> : Logger
    {
        public Logger(ILoggerFactory loggerFactory, IServiceFactory serviceFactory) 
            : base(loggerFactory, serviceFactory, typeof(TSource).FullName)
        {
        }
    }
}
