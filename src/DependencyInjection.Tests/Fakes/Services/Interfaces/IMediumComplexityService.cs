using System;

namespace FizzBuzz.DependencyInjection.Tests.Fakes.Services
{
    public interface IMediumComplexityService
    {
        IBasicService BasicService { get; }
        string InstanceId { get; }
        DateTime ResolvedOn { get; }
    }
}