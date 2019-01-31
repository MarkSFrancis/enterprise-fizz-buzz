using FizzBuzz.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzRequestHandler : IFizzBuzzRequestHandler
    {
        public FizzBuzzRequestHandler(IFizzBuzzService fizzBuzzService,
                                      IFizzBuzzQueryReader fizzBuzzQueryReader)
        {
            FizzBuzzService = fizzBuzzService;
            FizzBuzzQueryReader = fizzBuzzQueryReader;
        }

        public IFizzBuzzService FizzBuzzService { get; }
        public IFizzBuzzQueryReader FizzBuzzQueryReader { get; }

        public IEnumerable<string> HandleRequest(HttpRequest request)
        {
            var query = request.Query;

            var settings = FizzBuzzQueryReader.GetSettings(query);
            
            IEnumerable<string> result = FizzBuzzService.Play(settings.From, settings.Total);

            return result;
        }
    }
}
