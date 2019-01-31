using System;
using System.Diagnostics;
using FizzBuzz.DependencyInjection.Abstractions;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class NewGenericBenchmark<T> : IBenchmark where T : new()
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            var time = new Stopwatch();
            time.Start();
            for (int count = 0; count < executions; count++)
            {
                new T();
            }
            time.Stop();

            return new TimeSpan[] { time.Elapsed };
        }
    }
}
