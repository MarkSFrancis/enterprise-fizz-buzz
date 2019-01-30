using FizzBuzz.DependencyInjection.Helpers;
using FizzBuzz.DependencyInjection.Tests.Fakes.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace FizzBuzz.DependencyInjection.Tests
{
    public class ServiceFactoryTests
    {
        internal ServiceFactory DefaultFactory { get; }

        public ServiceFactoryTests()
        {
            var container = new ServiceContainer();

            container.AddTransient(factory => DateTime.UtcNow);
            container.AddTransient<IBasicService, BasicService>();
            container.AddSingleton<IMediumComplexityService, MediumComplexityService>();
            container.AddTransient<IComplexService, ComplexService>();
            container.AddTransient<Stream, MemoryStream>(factory => new MemoryStream());

            IDictionary<Type, RegisteredType> settings = container.ExportSettings();

            DefaultFactory = new ServiceFactory(InstanceFactory.Instance, settings);
        }

        [Test]
        public void NewFactory_WithNullInstanceFactory_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ServiceFactory(null, new Dictionary<Type, RegisteredType>()));
        }

        [Test]
        public void NewFactory_WithNullSettings_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ServiceFactory(InstanceFactory.Instance, null));
        }

        [Test]
        public void Get_UnresolvableDependancy_ThrowsKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => DefaultFactory.Get<long>());
        }

        [Test]
        public void Get_ResolvableDependancy_GetsDependancy()
        {
            var container = new ServiceContainer();
            container.AddTransient<decimal>();

            var factory = new ServiceFactory(InstanceFactory.Instance, container.ExportSettings());

            var instance = factory.Get<decimal>();

            Assert.AreEqual(0, instance);
        }

        [Test]
        public void Get_BasicService_GetsDependancy()
        {
            var instance = DefaultFactory.Get<IBasicService>();

            Assert.IsInstanceOf<BasicService>(instance);
        }

        [Test]
        public void Get_MediumComplexityService_GetsDependancy()
        {
            var instance = DefaultFactory.Get<IMediumComplexityService>();

            Assert.IsInstanceOf<MediumComplexityService>(instance);
        }

        [Test]
        public void Get_ComplexService_GetsDependancy()
        {
            var instance = DefaultFactory.Get<IComplexService>();

            Assert.IsInstanceOf<ComplexService>(instance);
        }

        [Test]
        public void GetTransient_Repeatedly_GetsDifferentInstances()
        {
            var instance1 = DefaultFactory.Get<IComplexService>();
            var instance2 = DefaultFactory.Get<IComplexService>();
            var instance3 = DefaultFactory.Get<IComplexService>();

            Assert.AreNotEqual(instance1.InstanceId, instance2.InstanceId);
            Assert.AreNotEqual(instance2.InstanceId, instance3.InstanceId);
            Assert.AreNotEqual(instance1.InstanceId, instance3.InstanceId);
        }

        [Test]
        public void GetSingleton_Repeatedly_GetsSameInstance()
        {
            var instance1 = DefaultFactory.Get<IMediumComplexityService>();
            var instance2 = DefaultFactory.Get<IMediumComplexityService>();
            var instance3 = DefaultFactory.Get<IMediumComplexityService>();

            Assert.AreEqual(instance1.InstanceId, instance2.InstanceId);
            Assert.AreEqual(instance2.InstanceId, instance3.InstanceId);
        }
    }
}
