using Castle.MicroKernel.Registration;
using Castle.Windsor;
using FizzBuzz.DependencyInjection.Abstractions;
using FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services;
using System;
using System.Diagnostics;

namespace FizzBuzz.DependencyInjection.Benchmarks
{
    public class CastleWindsorBenchmark : IBenchmark
    {
        public TimeSpan[] Run(IServiceFactory factory, int executions)
        {
            var container = new WindsorContainer();
            container.Register(Component.For<IBasicService>().ImplementedBy<BasicService>().LifestyleTransient());
            container.Register(Component.For<IMediumComplexityService>().ImplementedBy<MediumComplexityService>().LifestyleSingleton());
            container.Register(Component.For<IComplexService>().ImplementedBy<ComplexService>().LifestyleTransient());

            var times = new TimeSpan[3];

            var stp = new Stopwatch();
            stp.Start();

            for (var count = 0; count < executions; count++)
            {
                container.Resolve<IBasicService>();
            }

            stp.Stop();
            times[0] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                container.Resolve<IMediumComplexityService>();
            }

            stp.Stop();
            times[1] = stp.Elapsed;

            stp.Restart();

            for (var count = 0; count < executions; count++)
            {
                container.Resolve<IComplexService>();
            }

            stp.Stop();
            times[2] = stp.Elapsed;

            return times;
        }
    }
}
