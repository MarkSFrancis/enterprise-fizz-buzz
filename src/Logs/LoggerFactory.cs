using FizzBuzz.Logs.Outputs;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    internal class LoggerFactory : ILoggerFactory, ILoggerSetup
    {
        public LoggerFactory()
        {
            _logOutputs = new List<ILogOutput>();
            _sourceLogLevels = new Dictionary<string, LogLevel>();
        }

        private readonly IDictionary<string, LogLevel> _sourceLogLevels;

        private readonly List<ILogOutput> _logOutputs;

        public IEnumerable<ILogOutput> LogOutputs => _logOutputs;

        public LogLevel DefaultLogLevel { get; protected set; }

        public IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels => _sourceLogLevels;

        public ILoggerSetup AddOutput<T>() where T : ILogOutput, new()
        {
            _logOutputs.Add(new T());

            return this;
        }

        public ILoggerSetup SetMinimumLogLevel(LogLevel logLevel)
        {
            DefaultLogLevel = logLevel;

            return this;
        }

        public ILoggerSetup SetMinimumLogLevel(LogLevel logLevel, string sourcePrefix)
        {
            if (_sourceLogLevels.ContainsKey(sourcePrefix))
            {
                _sourceLogLevels[sourcePrefix] = logLevel;
            }
            else
            {
                _sourceLogLevels.Add(sourcePrefix, logLevel);
            }

            return this;
        }
    }
}
