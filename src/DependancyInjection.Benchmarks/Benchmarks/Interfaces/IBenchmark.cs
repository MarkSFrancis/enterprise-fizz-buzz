using System;
using FizzBuzz.DependencyInjection.Abstractions;

namespace FizzBuzz.DependencyInjection.Benchmarks.FizzBuzz.DependencyInjection.Benchmarks
{
    public interface IBenchmark
    {
        TimeSpan[] Run(IServiceFactory factory, int executions);
    }
}