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

        public FizzBuzzService() : this(FIZZ_BUZZ_DEFAULTS)
        {
        }

        public FizzBuzzService(IDictionary<int, string> stringReplacementsSettings)
        {
            StringReplacementsSettings = stringReplacementsSettings.OrderBy(s => s.Key);
        }

        public IOrderedEnumerable<KeyValuePair<int, string>> StringReplacementsSettings { get; }

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

            foreach (var stringReplacement in StringReplacementsSettings)
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
