using FizzBuzz.DependencyInjection;

namespace FizzBuzz.Console
{
    internal class Program
    {
        public static void Main()
        {
            var engine = new FizzBuzzEngine<Startup>();

            engine.Build();
            engine.Run();
        }
    }
}
