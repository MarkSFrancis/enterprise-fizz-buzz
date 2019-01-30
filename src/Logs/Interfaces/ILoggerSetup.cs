using FizzBuzz.Logs.Outputs;

namespace FizzBuzz.Logs
{
    public interface ILoggerSetup
    {
        ILoggerSetup AddOutput<T>() where T : ILogOutput;
        ILoggerSetup SetMinimumLogLevel(LogLevel logLevel);
        ILoggerSetup SetMinimumLogLevel(LogLevel logLevel, string sourceTypePrefix);
    }
}