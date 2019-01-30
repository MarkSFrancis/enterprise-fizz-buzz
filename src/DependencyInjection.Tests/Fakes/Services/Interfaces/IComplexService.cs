using System.IO;

namespace FizzBuzz.DependencyInjection.Tests.Fakes.Services
{
    public interface IComplexService
    {
        Stream DataStream { get; }
        string InstanceId { get; }
        IMediumComplexityService Service { get; }
    }
}