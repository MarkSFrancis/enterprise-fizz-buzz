using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using System;

namespace FizzBuzz.Api.Logs
{
    public class LoggerInterop : ILogger
    {
        public LoggerInterop(FizzBuzz.Logs.ILogger logger)
        {
            Logger = logger;
        }

        public FizzBuzz.Logs.ILogger Logger { get; }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);

            switch (logLevel)
            {
                case LogLevel.Trace:
                case LogLevel.Debug:
                    Logger.WriteTrace(message);
                    break;
                case LogLevel.Information:
                    Logger.WriteInfo(message);
                    break;
                case LogLevel.Warning:
                    Logger.WriteWarning(message);
                    break;
                case LogLevel.Error:
                case LogLevel.Critical:
                    Logger.WriteError(message);
                    break;
                case LogLevel.None:
                    break;
            }
        }
    }
}