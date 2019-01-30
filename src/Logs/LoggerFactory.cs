using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.Logs.Outputs;
using System;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    internal class LoggerFactory : ILoggerFactory, ILoggerSetup
    {
        public LoggerFactory(IServiceContainer serviceContainer)
        {
            _logOutputs = new HashSet<Type>();
            _sourceLogLevels = new Dictionary<string, LogLevel>();
            ServiceContainer = serviceContainer;
        }

        private readonly IDictionary<string, LogLevel> _sourceLogLevels;

        private readonly HashSet<Type> _logOutputs;

        public IEnumerable<Type> LogOutputs => _logOutputs;

        public LogLevel DefaultLogLevel { get; protected set; }

        public IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels => _sourceLogLevels;

        public IServiceContainer ServiceContainer { get; }

        public ILoggerSetup AddOutput<T>() where T : ILogOutput
        {
            if (!_logOutputs.Contains(typeof(T)))
            {
                _logOutputs.Add(typeof(T));
            }

            ServiceContainer.AddSingleton<T>();

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
