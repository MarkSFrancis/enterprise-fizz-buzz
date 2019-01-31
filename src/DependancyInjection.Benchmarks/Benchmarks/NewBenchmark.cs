using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using FizzBuzz.DependencyInjection.Abstractions;
using System;
using System.Diagnostics;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class NewBenchmark : IBenchmark
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            var time = new Stopwatch();
            time.Start();
            for (var count = 0; count < executions; count++)
            {
                new BasicService();
            }
            time.Stop();

            return new TimeSpan[] { time.Elapsed };
        }
    }
}
