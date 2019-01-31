using FizzBuzz.Logs.Outputs;

namespace FizzBuzz.Logs
{
    public interface ILoggerSetup
    {
        ILoggerSetup AddOutput<T>() where T : ILogOutput, new();
        ILoggerSetup SetMinimumLogLevel(LogLevel logLevel);
        ILoggerSetup SetMinimumLogLevel(LogLevel logLevel, string sourceTypePrefix);
    }
}