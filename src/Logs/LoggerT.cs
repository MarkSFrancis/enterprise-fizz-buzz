using FizzBuzz.DependencyInjection.Abstractions;

namespace FizzBuzz.Logs
{
    public class Logger<TSource> : Logger, ILogger<TSource>
    {
        public Logger(ILoggerFactory loggerFactory, IServiceFactory serviceFactory) 
            : base(loggerFactory, serviceFactory, typeof(TSource).FullName)
        {
        }
    }
}
