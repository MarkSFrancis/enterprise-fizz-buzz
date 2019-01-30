using FizzBuzz.DependencyInjection.Abstractions;
using NUnit.Framework;
using System;
using System.IO;

namespace FizzBuzz.DependencyInjection.Tests
{
    public class ServiceContainerTests
    {
        [Test]
        public void AddNewType_WithNullServiceType_ThrowsArgumentNullException()
        {
            var container = new ServiceContainer();

            Assert.Throws<ArgumentNullException>(() => container.Add(null, typeof(string), Lifetime.Singleton));
        }

        [Test]
        public void AddNewType_WithNullInstanceType_ThrowsArgumentNullException()
        {
            var container = new ServiceContainer();
            Type nullType = null;

            Assert.Throws<ArgumentNullException>(() => container.Add(typeof(string), nullType, Lifetime.Singleton));
        }

        [Test]
        public void AddNewType_WithInvalidInheritance_ThrowsArgumentException()
        {
            var container = new ServiceContainer();

            Assert.Throws<ArgumentException>(() => container.Add(typeof(string), typeof(int), Lifetime.Singleton));
        }

        [Test]
        public void AddNewType_WithValidArgs_DoesNotThrow()
        {
            var container = new ServiceContainer();

            Assert.DoesNotThrow(() => container.Add(typeof(string), typeof(string), Lifetime.Singleton));
        }

        [Test]
        public void AddNewFactory_WithNullServiceType_ThrowsArgumentNullException()
        {
            var container = new ServiceContainer();

            Assert.Throws<ArgumentNullException>(() => container.Add(null, _factory => null, Lifetime.Singleton));
        }

        [Test]
        public void AddNewFactory_WithNullFactory_ThrowsArgumentNullException()
        {
            var container = new ServiceContainer();
            Func<IServiceFactory, object> nullFactory = null;

            Assert.Throws<ArgumentNullException>(() => container.Add(typeof(string), nullFactory, Lifetime.Singleton));
        }

        [Test]
        public void AddNewFactory_WithValidArgs_DoesNotThrow()
        {
            var container = new ServiceContainer();

            Assert.DoesNotThrow(() => container.Add(typeof(string), _factory => string.Empty, Lifetime.Singleton));
        }

        [Test]
        public void ReplaceType_WithValidArgs_DoesNotThrow()
        {
            var container = new ServiceContainer();
            container.Add(typeof(string), typeof(string), Lifetime.Singleton);

            Assert.DoesNotThrow(() => container.Add(typeof(string), _factory => string.Empty, Lifetime.Singleton));
        }

        [Test]
        public void Add4Types_WithValidArgs_Adds4RegisteredTypes()
        {
            var container = new ServiceContainer();

            container.AddTransient<string>();
            container.AddTransient<Stream, MemoryStream>(factory => new MemoryStream());
            container.AddTransient<TextReader, StreamReader>();
            container.AddSingleton<decimal>();

            System.Collections.Generic.IDictionary<Type, Helpers.RegisteredType> settings = container.ExportSettings();

            // String
            Assert.IsTrue(settings.ContainsKey(typeof(string)));
            Helpers.RegisteredType stringRegisteredType = settings[typeof(string)];

            Assert.AreEqual(Lifetime.Transient, stringRegisteredType.Lifetime);
            Assert.IsFalse(stringRegisteredType.HasCustomConstructor);
            Assert.AreEqual(typeof(string), stringRegisteredType.ImplementationType);

            // Stream
            Assert.IsTrue(settings.ContainsKey(typeof(Stream)));
            Helpers.RegisteredType streamRegisteredType = settings[typeof(Stream)];

            Assert.AreEqual(Lifetime.Transient, streamRegisteredType.Lifetime);
            Assert.IsTrue(streamRegisteredType.HasCustomConstructor);
            Assert.IsNull(streamRegisteredType.ImplementationType);
            Assert.IsNotNull(streamRegisteredType.CustomConstructor);

            // TextReader
            Assert.IsTrue(settings.ContainsKey(typeof(TextReader)));
            Helpers.RegisteredType textReaderRegisteredType = settings[typeof(TextReader)];

            Assert.AreEqual(Lifetime.Transient, textReaderRegisteredType.Lifetime);
            Assert.IsFalse(textReaderRegisteredType.HasCustomConstructor);
            Assert.AreEqual(typeof(StreamReader), textReaderRegisteredType.ImplementationType);

            // Decimal
            Assert.IsTrue(settings.ContainsKey(typeof(decimal)));
            Helpers.RegisteredType decimalRegisteredType = settings[typeof(decimal)];

            Assert.AreEqual(Lifetime.Singleton, decimalRegisteredType.Lifetime);
            Assert.IsFalse(decimalRegisteredType.HasCustomConstructor);
            Assert.AreEqual(typeof(decimal), decimalRegisteredType.ImplementationType);
        }
    }
}