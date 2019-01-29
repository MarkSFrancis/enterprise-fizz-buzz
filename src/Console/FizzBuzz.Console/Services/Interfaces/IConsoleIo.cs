using System.Collections.Generic;

namespace FizzBuzz.Console.Services
{
    public interface IConsoleIo
    {
        int GetIntegerFromUser(int minValue = int.MinValue, int maxValue = int.MaxValue);
        void Write(string text = "");
        void WriteLine(string text = "");
        void WriteCollection(IEnumerable<string> values, string delimiter);
    }
}