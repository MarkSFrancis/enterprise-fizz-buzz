using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using Ninject;
using System;
using System.Diagnostics;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class NinjectDiContainerBenchmarks : IBenchmark
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            IKernel kernel = new Ninject.StandardKernel();

            kernel.Bind<IBasicService>().To<BasicService>();
            kernel.Bind<IMediumComplexityService>().To<MediumComplexityService>().InSingletonScope();
            kernel.Bind<IComplexService>().To<ComplexService>();

            var times = new TimeSpan[3];

            var stp = new Stopwatch();
            stp.Start();

            for (var count = 0; count < executions; count++)
            {
                kernel.Get<IBasicService>();
            }

            stp.Stop();
            times[0] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                kernel.Get<IMediumComplexityService>();
            }

            stp.Stop();
            times[1] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                kernel.Get<IComplexService>();
            }

            stp.Stop();
            times[2] = stp.Elapsed;

            return times;
        }
    }
}
