using FizzBuzz.Api.Models;
using Microsoft.AspNetCore.Http;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzQueryReader : IFizzBuzzQueryReader
    {
        public FizzBuzzSettingsModel GetSettings(IQueryCollection query)
        {
            var model = new FizzBuzzSettingsModel();
            if (query is null)
            {
                return model;
            }

            var fromString = query["from"];

            if (int.TryParse(fromString, out var fromInt))
            {
                model.From = fromInt;
            }

            var totalString = query["total"];

            if (int.TryParse(totalString, out var totalInt))
            {
                model.Total = totalInt;
            }

            return model;
        }
    }
}
