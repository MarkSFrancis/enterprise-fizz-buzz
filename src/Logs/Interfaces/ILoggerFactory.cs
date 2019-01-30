using System;
using System.Collections.Generic;
using Logs.Outputs;

namespace FizzBuzz.Logs
{
    public interface ILoggerFactory
    {
        LogLevel DefaultLogLevel { get; }
        IEnumerable<Type> LogOutputs { get; }
        IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels { get; }

        LoggerFactory AddOutput<T>() where T : ILogOutput;
        LoggerFactory SetMinimumLogLevel(LogLevel logLevel);
        LoggerFactory SetMinimumLogLevel(LogLevel logLevel, string sourcePrefix);
    }
}