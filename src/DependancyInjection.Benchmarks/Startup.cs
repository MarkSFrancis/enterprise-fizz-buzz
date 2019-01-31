using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using System;
using System.Threading.Tasks;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class Startup : IStartup
    {
        public void AddServices(IServiceContainer container)
        {
            container.AddTransient<IBasicService, BasicService>();
            container.AddTransient<IComplexService, ComplexService>();
            container.AddSingleton<IMediumComplexityService, MediumComplexityService>();
        }

        public Task Run(IServiceFactory factory)
        {
            var executions = 1000000;

            TimeSpan[] customContainerTimes = new CustomDiContainerBenchmark().Run(factory, executions);

            Console.WriteLine("Custom Container: ");
            foreach (TimeSpan time in customContainerTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            TimeSpan[] microsoftContainerTimes = new MicrosoftDiContainerBenchmark().Run(factory, executions);

            Console.WriteLine("Microsoft Container: ");
            foreach (TimeSpan time in microsoftContainerTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            TimeSpan[] ninjectContainerTimes = new NinjectDiContainerBenchmarks().Run(factory, executions);

            Console.WriteLine("Ninject Container: ");
            foreach (TimeSpan time in ninjectContainerTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            TimeSpan[] newGenericTimes = new NewGenericBenchmark<GenericService<string>>().Run(factory, executions);

            Console.WriteLine("New Generic: ");
            foreach (TimeSpan time in newGenericTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            TimeSpan[] newTimes = new NewBenchmark().Run(factory, executions);

            Console.WriteLine("New: ");
            foreach (TimeSpan time in newTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();


            return Task.CompletedTask;
        }
    }
}
