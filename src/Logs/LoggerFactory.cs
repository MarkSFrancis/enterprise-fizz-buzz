using Logs.Outputs;
using System;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    public class LoggerFactory : ILoggerFactory
    {
        public LoggerFactory()
        {
            _logOutputs = new HashSet<Type>();
            _sourceLogLevels = new Dictionary<string, LogLevel>();
        }

        private readonly IDictionary<string, LogLevel> _sourceLogLevels;

        private readonly HashSet<Type> _logOutputs;

        public IEnumerable<Type> LogOutputs => _logOutputs;

        public LogLevel DefaultLogLevel { get; protected set; }

        public IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels => _sourceLogLevels;

        public LoggerFactory AddOutput<T>() where T : ILogOutput
        {
            if (!_logOutputs.Contains(typeof(T)))
            {
                _logOutputs.Add(typeof(T));
            }

            return this;
        }

        public LoggerFactory SetMinimumLogLevel(LogLevel logLevel)
        {
            DefaultLogLevel = logLevel;

            return this;
        }

        public LoggerFactory SetMinimumLogLevel(LogLevel logLevel, string sourcePrefix)
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
