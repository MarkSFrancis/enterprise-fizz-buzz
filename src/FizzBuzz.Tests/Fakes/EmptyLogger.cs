using FizzBuzz.Logs;
using FizzBuzz.Services;

namespace FizzBuzz.Tests.Fakes
{
    public class EmptyLogger : ILogger<FizzBuzzService>
    {
        public static EmptyLogger Instance => new EmptyLogger();

        public void WriteError(string message)
        {
        }

        public void WriteInfo(string message)
        {
        }

        public void WriteTrace(string message)
        {
        }

        public void WriteWarning(string message)
        {
        }
    }
}
