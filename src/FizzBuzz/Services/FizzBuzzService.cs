using FizzBuzz.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FizzBuzz.Services
{
    /// <summary>
    /// Provides a service which replaces numbers with a paired string when they're divisible by that number
    /// </summary>
    public class FizzBuzzService : IFizzBuzzService
    {
        private static readonly IDictionary<int, string> FIZZ_BUZZ_DEFAULTS = new Dictionary<int, string>
        {
            { 3, "Fizz" },
            { 5, "Buzz" }
        };

        public FizzBuzzService(ILogger<FizzBuzzService> logger) : this(logger, FIZZ_BUZZ_DEFAULTS)
        {
        }

        public FizzBuzzService(ILogger<FizzBuzzService> logger, IDictionary<int, string> stringReplacementsSettings)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (stringReplacementsSettings is null)
            {
                throw new ArgumentNullException(nameof(stringReplacementsSettings));
            }

            var replacementsInOrder = new List<KeyValuePair<int, string>>(stringReplacementsSettings.Count);

            replacementsInOrder = stringReplacementsSettings.Select(s =>
            {
                if (s.Key < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(stringReplacementsSettings), $"{nameof(stringReplacementsSettings)} cannot have a key less than zero for any entries");
                }
                if (s.Value is null)
                {
                    throw new ArgumentException($"{nameof(stringReplacementsSettings)} cannot have a null value for any entries", nameof(stringReplacementsSettings));
                }

                return s;
            })
            .OrderBy(s => s.Key)
            .ToList();

            StringReplacementsSettings = replacementsInOrder;
        }

        public IEnumerable<KeyValuePair<int, string>> StringReplacementsSettings { get; }
        public ILogger<FizzBuzzService> Logger { get; }

        /// <summary>
        /// Replaces each number with its modulus from the <see cref="StringReplacementsSettings"/> indefinitely, starting at <paramref name="startAt"/>
        /// </summary>
        /// <param name="startAt">The number at which replacements should begin</param>
        /// <returns>The replaced sequence</returns>
        public IEnumerable<string> Play(int startAt = 1)
        {
            if (startAt < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startAt), $"{nameof(startAt)} cannot be less than zero");
            }

            Logger.WriteInfo("Running FizzBuzz from " + startAt);

            while (true)
            {
                var replacement = GetReplacement(startAt++);

                yield return replacement;
            }
        }

        /// <summary>
        /// Returns fizz buzz indefinitely, starting at <paramref name="startAt"/>
        /// </summary>
        /// <param name="startAt">The number at which fizz buzz should begin</param>
        /// <returns>The Fizz Buzz sequence</returns>
        public IEnumerable<string> Play(int startAt, int totalToCount)
        {
            return Play(startAt).Take(totalToCount);
        }

        private string GetReplacement(int number)
        {
            var hasReplacement = false;
            var replacement = new StringBuilder();

            foreach (KeyValuePair<int, string> stringReplacement in StringReplacementsSettings)
            {
                if (number % stringReplacement.Key == 0)
                {
                    replacement.Append(stringReplacement.Value);
                    hasReplacement = true;
                }
            }

            return hasReplacement ? replacement.ToString() : number.ToString();
        }
    }
}
