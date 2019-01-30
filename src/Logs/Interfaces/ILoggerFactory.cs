using System;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    public interface ILoggerFactory
    {
        LogLevel DefaultLogLevel { get; }
        IEnumerable<Type> LogOutputs { get; }
        IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels { get; }
    }
}