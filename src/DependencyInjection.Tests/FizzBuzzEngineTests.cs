using FizzBuzz.DependencyInjection.Tests.Fakes;
using NUnit.Framework;
using System;

namespace FizzBuzz.DependencyInjection.Tests
{
    public class FizzBuzzEngineTests
    {
        [SetUp]
        public void ResetStartupStateWatchers()
        {
            SimpleFakeStartup.ResetState();
            ComplexFakeStartup.ResetState();
        }

        [Test]
        public void NewFizzBuzzEngine_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => _ = new FizzBuzzEngine<SimpleFakeStartup>());
        }

        [Test]
        public void NewFizzBuzzEngine_DoesNotAddServicesOrRun()
        {
            new FizzBuzzEngine<SimpleFakeStartup>();
            Assert.IsFalse(SimpleFakeStartup.AddedServices);
            Assert.IsFalse(SimpleFakeStartup.RanToCompletion);
        }

        [Test]
        public void Build_CallsAddServicesInStartup()
        {
            var engine = new FizzBuzzEngine<SimpleFakeStartup>();

            engine.Build();

            Assert.IsTrue(SimpleFakeStartup.AddedServices);
        }

        [Test]
        public void Run_AfterBuild_CallsRunInStartupAndWaitsForCompletion()
        {
            var engine = new FizzBuzzEngine<SimpleFakeStartup>();

            engine.Build();
            engine.Run();

            Assert.IsTrue(SimpleFakeStartup.RanToCompletion);
        }

        [Test]
        public void Run_BeforeBuild_ThrowsInvalidOperationException()
        {
            var engine = new FizzBuzzEngine<SimpleFakeStartup>();
            
            Assert.Throws<InvalidOperationException>(() => engine.Run());
        }

        [Test]
        public void NewFizzBuzzEngine_WithComplexStartup_DoesNotAddServicesOrRun()
        {
            new FizzBuzzEngine<ComplexFakeStartup>();
            Assert.IsFalse(ComplexFakeStartup.AddedServices);
            Assert.IsFalse(ComplexFakeStartup.RanToCompletion);
        }
        
        [Test]
        public void Build_WithComplexStartup_InjectsPrerequisites()
        {
            var engine = new FizzBuzzEngine<ComplexFakeStartup>();

            engine.Build();

            Assert.IsNotNull(ComplexFakeStartup.FizzBuzzService);
            Assert.IsNotNull(ComplexFakeStartup.Factory);
            Assert.IsTrue(ComplexFakeStartup.AddedServices);
        }

        [Test]
        public void Run_WithComplexStartup_RunsToCompletion()
        {
            var engine = new FizzBuzzEngine<ComplexFakeStartup>();

            engine.Build();
            engine.Run();

            Assert.IsTrue(ComplexFakeStartup.RanToCompletion);
        }
    }
}
