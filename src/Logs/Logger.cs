using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.Logs.Outputs;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    public class Logger : ILogger
    {
        public string Source { get; set; }

        private ILoggerFactory LogFactory { get; set; }

        public Logger(ILoggerFactory logFactory)
        {
            Source = null;
            LogFactory = logFactory;
        }

        public Logger(ILoggerFactory logFactory, string source)
            : this(logFactory)
        {
            Source = source;
        }

        public void Write(LogLevel level, string message)
        {
            if (!IsAtLeastMinimumLogLevel(level))
            {
                return;
            }

            IEnumerable<ILogOutput> outputs = LogFactory.LogOutputs;

            switch (level)
            {
                case LogLevel.Trace:
                    foreach (ILogOutput output in outputs)
                    {
                        output.WriteTrace(message);
                    }
                    break;
                case LogLevel.Info:
                    foreach (ILogOutput output in outputs)
                    {
                        output.WriteInfo(message);
                    }
                    break;
                case LogLevel.Warning:
                    foreach (ILogOutput output in outputs)
                    {
                        output.WriteWarning(message);
                    }
                    break;
                case LogLevel.Error:
                    foreach (ILogOutput output in outputs)
                    {
                        output.WriteError(message);
                    }
                    break;
            }
        }

        public void WriteTrace(string message)
        {
            Write(LogLevel.Trace, message);
        }

        public void WriteInfo(string message)
        {
            Write(LogLevel.Info, message);
        }

        public void WriteError(string message)
        {
            Write(LogLevel.Error, message);
        }

        public void WriteWarning(string message)
        {
            Write(LogLevel.Warning, message);
        }

        private bool IsAtLeastMinimumLogLevel(LogLevel level)
        {
            LogLevel minimumLevel = GetConfiguredLogLevel();

            var isAtLeastMinimum = minimumLevel <= level;

            return isAtLeastMinimum;
        }

        private LogLevel GetConfiguredLogLevel()
        {
            if (Source != null)
            {
                // Validate minimum log level is reached against source
                foreach (System.Collections.Generic.KeyValuePair<string, LogLevel> sourceLevel in LogFactory.SourceLogLevels)
                {
                    if (Source.StartsWith(sourceLevel.Key))
                    {
                        return sourceLevel.Value;
                    }
                }
            }

            return LogFactory.DefaultLogLevel;
        }
    }
}
