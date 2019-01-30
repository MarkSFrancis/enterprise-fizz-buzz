using System;

namespace FizzBuzz.DependencyInjection.Tests.Fakes.Services
{
    public class BasicService : IBasicService
    {
        public BasicService(DateTime dateTime)
        {
            InstanceId = Guid.NewGuid().ToString();
            DateTime = dateTime;
        }

        public string InstanceId { get; }

        public DateTime DateTime { get; }
    }
}
