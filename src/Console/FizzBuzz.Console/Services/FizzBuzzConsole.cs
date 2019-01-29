using System;
using System.Collections.Generic;

namespace FizzBuzz.Console.Services
{
    public class FizzBuzzConsole : IFizzBuzzConsole
    {
        public FizzBuzzConsole(IConsoleIo console)
        {
            Console = console;
        }

        protected IConsoleIo Console { get; }

        public int GetNumberFrom()
        {
            Console.WriteLine("Please enter a number from which to start fizz buzz");
            var from = Console.GetIntegerFromUser(0);

            return from;
        }

        public int GetNumberUpTo()
        {
            Console.WriteLine("Please enter the number of values to count");
            var from = Console.GetIntegerFromUser(0);

            return from;
        }

        public void OutputFizzBuzz(IEnumerable<string> sequence)
        {
            Console.WriteCollection(sequence, Environment.NewLine);
        }
    }
}
