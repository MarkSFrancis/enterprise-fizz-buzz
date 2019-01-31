using System;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    internal class Program
    {
        private static void Main()
        {
            var engine = new FizzBuzzEngine<Startup>();
            engine.Build();
            engine.Run();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
