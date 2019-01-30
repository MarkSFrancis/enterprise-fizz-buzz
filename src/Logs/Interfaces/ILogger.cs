using FizzBuzz.Logs.Outputs;

namespace FizzBuzz.Logs
{
    public interface ILogger : ILogOutput
    {
    }

    public interface ILogger<TSource> : ILogger
    {
    }
}
