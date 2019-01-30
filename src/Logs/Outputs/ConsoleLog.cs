using System;

namespace FizzBuzz.Logs.Outputs
{
    public class ConsoleLog : ILogOutput
    {
        public void WriteError(string message)
        {
            WriteLineInColor(message, ConsoleColor.Red);
        }

        public void WriteInfo(string message)
        {
            WriteLineInColor(message, ConsoleColor.White);
        }

        public void WriteTrace(string message)
        {
            WriteLineInColor(message, ConsoleColor.Cyan);
        }

        public void WriteWarning(string message)
        {
            WriteLineInColor(message, ConsoleColor.Yellow);
        }

        private void WriteLineInColor(string message, ConsoleColor color)
        {
            var initialColor = Console.ForegroundColor;
            
            Console.ForegroundColor = color;
            Console.WriteLine(message);

            // Reset color
            Console.ForegroundColor = initialColor;
        }
    }
}
