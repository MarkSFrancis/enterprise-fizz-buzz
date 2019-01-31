using FizzBuzz.DependencyInjection.Benchmarks.FizzBuzz.DependencyInjection.Benchmarks;
using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using FizzBuzz.DependencyInjection.Abstractions;
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

            var customContainerTimes = new CustomDiContainerBenchmark().Run(factory, executions);

            Console.WriteLine("Custom Container: ");
            foreach (var time in customContainerTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            var microsoftContainerTimes = new MicrosoftDiContainerBenchmark().Run(factory, executions);

            Console.WriteLine("Microsoft Container: ");
            foreach (var time in microsoftContainerTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            var newGenericTimes = new NewGenericBenchmark<GenericService<string>>().Run(factory, executions);

            Console.WriteLine("New Generic: ");
            foreach (var time in newGenericTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();

            var newTimes = new NewBenchmark().Run(factory, executions);

            Console.WriteLine("New: ");
            foreach (var time in newTimes)
            {
                Console.WriteLine(time);
            }
            Console.WriteLine();


            return Task.CompletedTask;
        }
    }
}
