using FizzBuzz.Logs;
using System;
using System.Collections.Generic;

namespace FizzBuzz.Console.Services
{
    public class FizzBuzzConsole : IFizzBuzzConsole
    {
        public FizzBuzzConsole(IConsoleIo console, ILogger<FizzBuzzConsole> logger)
        {
            Console = console;
            Logger = logger;
        }

        protected IConsoleIo Console { get; }
        public ILogger<FizzBuzzConsole> Logger { get; }

        public int GetNumberFrom()
        {
            Console.WriteLine("Please enter a number from which to start fizz buzz");
            var from = Console.GetIntegerFromUser(0);

            return from;
        }

        public int GetNumberUpTo()
        {
            Console.WriteLine("Please enter the number to count up to");
            var from = Console.GetIntegerFromUser(0);

            return from;
        }

        public void OutputFizzBuzz(IEnumerable<string> sequence)
        {
            Logger.WriteInfo("Running FizzBuzz...");
            Console.WriteCollection(sequence, Environment.NewLine);
            Console.WriteLine();
        }

        public bool PlayAgain()
        {
            Console.Write("Play again? (y/n): ");
            var playAgain = Console.YesNo();

            return playAgain;
        }
    }
}
