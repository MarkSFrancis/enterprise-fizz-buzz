﻿namespace FizzBuzz.DependencyInjection.Benchmarks.Fakes.Services
{
    public interface IMediumComplexityService
    {
        IBasicService BasicService { get; }
    }
}