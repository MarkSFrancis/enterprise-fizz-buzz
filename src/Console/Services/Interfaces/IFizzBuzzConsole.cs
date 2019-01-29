using System.Collections.Generic;

namespace FizzBuzz.Console.Services
{
    public interface IFizzBuzzConsole
    {
        int GetNumberFrom();
        int GetNumberUpTo();
        void OutputFizzBuzz(IEnumerable<string> sequence);
    }
}