using System;
using System.IO;

namespace FizzBuzz.DependencyInjection.Tests.Services
{
    public class ComplexService : IComplexService
    {
        public ComplexService(IMediumComplexityService service, Stream dataStream)
        {
            InstanceId = Guid.NewGuid().ToString();
            Service = service;
            DataStream = dataStream;
        }

        public string InstanceId { get; }

        public IMediumComplexityService Service { get; }

        public Stream DataStream { get; }
    }
}
