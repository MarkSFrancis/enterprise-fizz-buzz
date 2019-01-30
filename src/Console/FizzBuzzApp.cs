using FizzBuzz.Console.Services;
using FizzBuzz.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FizzBuzz.Console
{
    public class FizzBuzzApp : IFizzBuzzApp
    {
        public FizzBuzzApp(IFizzBuzzConsole fizzBuzzConsole, IConsoleIo console, IFizzBuzzService fizzBuzzService)
        {
            FizzBuzzConsole = fizzBuzzConsole;
            Console = console;
            FizzBuzzService = fizzBuzzService;
        }

        public IFizzBuzzConsole FizzBuzzConsole { get; }
        public IConsoleIo Console { get; }
        public IFizzBuzzService FizzBuzzService { get; }

        public Task Run()
        {
            return Task.Run(() =>
            {
                do
                {
                    Console.Clear();
                    var from = FizzBuzzConsole.GetNumberFrom();
                    var to = FizzBuzzConsole.GetNumberUpTo();

                    if (from > to) continue;

                    Console.WriteLine();

                    var fizzBuzzResult = FizzBuzzService.Play(from, (to - from) + 1);

                    FizzBuzzConsole.OutputFizzBuzz(fizzBuzzResult);
                    
                    Console.WriteLine();

                } while (FizzBuzzConsole.PlayAgain());
            });
        }
    }
}
