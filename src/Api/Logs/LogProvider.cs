using Microsoft.Extensions.Logging;

namespace FizzBuzz.Api.Logs
{
    public class LogProvider : ILoggerProvider
    {
        public LogProvider(FizzBuzz.Logs.ILoggerFactory logsFactory)
        {
            LogsFactory = logsFactory;
        }

        public FizzBuzz.Logs.ILoggerFactory LogsFactory { get; }
        public ILogger CreateLogger(string categoryName)
        {
            var logger = new FizzBuzz.Logs.Logger(LogsFactory);
            return new LoggerInterop(logger);
        }

        public void Dispose()
        {
        }
    }
}
