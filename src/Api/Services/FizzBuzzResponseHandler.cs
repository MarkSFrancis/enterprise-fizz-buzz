using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FizzBuzz.Api.Services
{
    public class FizzBuzzResponseHandler : IFizzBuzzResponseHandler
    {
        private const string ApplicationJsonContentType = "application/json";

        public FizzBuzzResponseHandler(IJsonSerializerService jsonSerializerService)
        {
            JsonSerializerService = jsonSerializerService;
        }

        public IJsonSerializerService JsonSerializerService { get; }

        public async Task WriteResponse(HttpResponse response, IEnumerable<string> fizzBuzzSequence)
        {
            response.ContentType = ApplicationJsonContentType;

            var serialized = JsonSerializerService.Serialize(fizzBuzzSequence);

            await response.WriteAsync(serialized);
        }
    }
}
