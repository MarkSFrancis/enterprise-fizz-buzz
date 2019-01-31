using FizzBuzz.Services;
using FizzBuzz.Tests.Fakes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz.Tests.Services
{
    public class FizzBuzzServiceTests
    {
        [Test]
        public void New_WithNullSettings_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new FizzBuzzService(null));
        }

        [Test]
        public void New_WithEmptySettings_UsesDefaultFizzBuzzReplacements()
        {
            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            CollectionAssert.IsNotEmpty(fizzBuzz.StringReplacementsSettings);
        }

        [Test]
        public void New_WithCustomSettings_UsesCustomSettings()
        {
            var settings = new Dictionary<int, string>
            {
                { 7, "Asdf" }
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance, settings);

            CollectionAssert.AreEqual(settings, fizzBuzz.StringReplacementsSettings);
        }

        [Test]
        public void New_WithCustomSettingsInWrongOrder_CorrectsOrder()
        {
            var settings = new Dictionary<int, string>
            {
                { 7, "Asdf" },
                { 1, "Test" }
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance, settings);

            CollectionAssert.AreEqual(settings.OrderBy(s => s.Key), fizzBuzz.StringReplacementsSettings);
        }

        [Test]
        public void New_WithKeyLessThanZero_ThrowsArgumentOutOfRangeException()
        {
            var settings = new Dictionary<int, string>
            {
                { -1, "Asdf" }
            };

            Assert.Throws<ArgumentOutOfRangeException>(() => new FizzBuzzService(EmptyLogger.Instance, settings));
        }

        [Test]
        public void New_WithValueNullString_ThrowsArgumentException()
        {
            var settings = new Dictionary<int, string>
            {
                { 1, null }
            };

            Assert.Throws<ArgumentException>(() => new FizzBuzzService(EmptyLogger.Instance, settings));
        }

        [Test]
        public void Play_FromMinus1WithNoEnd_ThrowsArgumentOutOfRangeException()
        {
            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            Assert.Throws<ArgumentOutOfRangeException>(() => fizzBuzz.Play(-1).First());
        }

        [Test]
        public void Play_From1WithNoEnd_PlaysFromStart()
        {
            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            var result = fizzBuzz.Play(1).First();

            Assert.AreEqual("1", result);
        }

        [Test]
        public void Play_From1WithNoEnd_PlaysIndefinitely()
        {
            var expected = new List<string>
            {
                "1", "2", "Fizz", "4", "Buzz",
                "Fizz", "7", "8", "Fizz", "Buzz",
                "11", "Fizz", "13", "14", "FizzBuzz"
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            IEnumerable<string> result = fizzBuzz.Play(1).Take(15);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Play_From10WithNoEnd_PlaysIndefinitely()
        {
            var expected = new List<string>
            {
                "Buzz", "11", "Fizz", "13", "14",
                "FizzBuzz", "16", "17", "Fizz", "19",
                "Buzz", "Fizz", "22", "23", "Fizz"
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            IEnumerable<string> result = fizzBuzz.Play(10).Take(15);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Play_From1For10Numbers_PlaysFromStart()
        {
            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            var result = fizzBuzz.Play(1, 10).First();

            Assert.AreEqual("1", result);
        }

        [Test]
        public void Play_From1For10Numbers_PlaysUpTo10()
        {
            var expected = new List<string>
            {
                "1", "2", "Fizz", "4", "Buzz",
                "Fizz", "7", "8", "Fizz", "Buzz"
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            IEnumerable<string> result = fizzBuzz.Play(1, 10);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Play_From10For10Numbers_PlaysUpTo10()
        {
            var expected = new List<string>
            {
                "Buzz", "11", "Fizz", "13", "14",
                "FizzBuzz", "16", "17", "Fizz", "19"
            };

            var fizzBuzz = new FizzBuzzService(EmptyLogger.Instance);

            IEnumerable<string> result = fizzBuzz.Play(10, 10);

            Assert.AreEqual(expected, result);
        }
    }
}
