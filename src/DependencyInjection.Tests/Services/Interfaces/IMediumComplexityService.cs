using System;

namespace FizzBuzz.DependencyInjection.Tests.Services
{
    public interface IMediumComplexityService
    {
        IBasicService BasicService { get; }
        string InstanceId { get; }
        DateTime ResolvedOn { get; }
    }
}