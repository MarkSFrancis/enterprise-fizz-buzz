using FizzBuzz.Logs.Outputs;
using System.Collections.Generic;

namespace FizzBuzz.Logs
{
    public interface ILoggerFactory
    {
        LogLevel DefaultLogLevel { get; }
        IEnumerable<ILogOutput> LogOutputs { get; }
        IEnumerable<KeyValuePair<string, LogLevel>> SourceLogLevels { get; }
    }
}