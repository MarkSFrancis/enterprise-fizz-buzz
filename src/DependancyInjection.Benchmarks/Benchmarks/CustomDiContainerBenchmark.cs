using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using FizzBuzz.DependencyInjection.Abstractions;
using System;
using System.Diagnostics;

namespace FizzBuzz.DependencyInjection.Benchmarks.FizzBuzz.DependencyInjection.Benchmarks
{
    public class CustomDiContainerBenchmark : IBenchmark
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            var times = new TimeSpan[3];

            var stp = new Stopwatch();
            stp.Start();

            for (var count = 0; count < executions; count++)
            {
                factory.Get<IBasicService>();
            }

            stp.Stop();
            times[0] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                factory.Get<IMediumComplexityService>();
            }

            stp.Stop();
            times[1] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                factory.Get<IComplexService>();
            }

            stp.Stop();
            times[2] = stp.Elapsed;

            return times;
        }
    }
}
