using FizzBuzz.DependencyInjection.Abstractions;

namespace FizzBuzz.Logs
{
    public class Logger<TSource> : Logger, ILogger<TSource>
    {
        public Logger(ILoggerFactory loggerFactory) 
            : base(loggerFactory, typeof(TSource).FullName)
        {
        }
    }
}
