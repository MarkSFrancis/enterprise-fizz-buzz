using System;
using System.Collections.Generic;

namespace FizzBuzz.Console.Services
{
    using Console = System.Console;

    public class ConsoleIo : IConsoleIo
    {
        public void WriteLine(string text = "")
        {
            if (text is null)
            {
                throw new ArgumentNullException(text);
            }

            Console.WriteLine(text);
        }

        public void Write(string text = "")
        {
            if (text is null)
            {
                throw new ArgumentNullException(text);
            }

            Console.Write(text);
        }

        public void WriteCollection(IEnumerable<string> values, string delimiter)
        {
            using (IEnumerator<string> enumerator = values.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                {
                    return;
                }

                do
                {
                    Console.Write(enumerator.Current);

                    if (!enumerator.MoveNext())
                    {
                        return;
                    }

                    Console.Write(delimiter);
                } while (true);
            }
        }

        public int GetIntegerFromUser(int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            string lastValidationMessage = null;

            do
            {
                var userInput = Console.ReadLine();

                if (int.TryParse(userInput, out var number))
                {
                    return number;
                }

                ClearEnteredText(userInput.Length + Environment.NewLine.Length);

                if (lastValidationMessage != null)
                {
                    ClearEnteredText(lastValidationMessage.Length + Environment.NewLine.Length);
                }

                lastValidationMessage = $"The value \"{userInput}\" is not a valid integer. Please enter an integer between {minValue} and {maxValue}";

                Console.WriteLine(lastValidationMessage);
            } while (true);
        }

        private void ClearEnteredText(int textLength)
        {
            Console.Write(new string('\b', textLength));
        }
    }
}
