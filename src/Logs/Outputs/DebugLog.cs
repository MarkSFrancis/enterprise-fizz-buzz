using System.Diagnostics;

namespace FizzBuzz.Logs.Outputs
{
    public class DebugLog : ILogOutput
    {
        public void WriteError(string message)
        {
            WriteLine(message);
        }

        public void WriteInfo(string message)
        {
            WriteLine(message);
        }

        public void WriteTrace(string message)
        {
            WriteLine(message);
        }

        public void WriteWarning(string message)
        {
            WriteLine(message);
        }

        private void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
