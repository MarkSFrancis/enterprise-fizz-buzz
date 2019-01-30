using System;

namespace FizzBuzz.DependencyInjection.Tests.Services
{
    public class BasicService : IBasicService
    {
        public BasicService(DateTime dt)
        {
            InstanceId = Guid.NewGuid().ToString();
        }

        public string InstanceId { get; }
    }
}
