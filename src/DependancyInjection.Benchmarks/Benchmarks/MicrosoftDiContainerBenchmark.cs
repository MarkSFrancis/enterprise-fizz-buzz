using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using FizzBuzz.DependencyInjection.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class MicrosoftDiContainerBenchmark : IBenchmark
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            var newMicrosoftFactory = new ServiceCollection();
            newMicrosoftFactory.AddTransient<IBasicService, BasicService>();
            newMicrosoftFactory.AddSingleton<IMediumComplexityService, MediumComplexityService>();
            newMicrosoftFactory.AddTransient<IComplexService, ComplexService>();

            var provider = newMicrosoftFactory.BuildServiceProvider();

            var times = new TimeSpan[3];

            var stp = new Stopwatch();
            stp.Start();

            for (var count = 0; count < executions; count++)
            {
                provider.GetRequiredService<IBasicService>();
            }

            stp.Stop();
            times[0] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                provider.GetRequiredService<IMediumComplexityService>();
            }

            stp.Stop();
            times[1] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                provider.GetRequiredService<IComplexService>();
            }

            stp.Stop();
            times[2] = stp.Elapsed;

            return times;
        }
    }
}
