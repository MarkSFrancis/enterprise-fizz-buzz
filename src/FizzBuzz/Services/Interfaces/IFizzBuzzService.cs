using System.Collections.Generic;
using System.Linq;

namespace FizzBuzz.Services
{
    public interface IFizzBuzzService
    {
        IOrderedEnumerable<KeyValuePair<int, string>> StringReplacementsSettings { get; }

        IEnumerable<string> Play(int startAt = 1);
        IEnumerable<string> Play(int startAt, int totalToCount);
    }
}