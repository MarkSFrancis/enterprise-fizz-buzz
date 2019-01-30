using System;

namespace FizzBuzz.DependencyInjection.Tests.Fakes.Services
{
    public class MediumComplexityService : IMediumComplexityService
    {
        public MediumComplexityService(IBasicService basicService, DateTime resolvedOn)
        {
            InstanceId = Guid.NewGuid().ToString();
            BasicService = basicService;
            ResolvedOn = resolvedOn;
        }

        public string InstanceId { get; }

        public IBasicService BasicService { get; }

        public DateTime ResolvedOn { get; }
    }
}
