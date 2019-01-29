using System.Collections.Generic;

namespace FizzBuzz.Services
{
    public interface IFizzBuzzService
    {
        IEnumerable<KeyValuePair<int, string>> StringReplacementsSettings { get; }

        IEnumerable<string> Play(int startAt = 1);
        IEnumerable<string> Play(int startAt, int totalToCount);
    }
}
