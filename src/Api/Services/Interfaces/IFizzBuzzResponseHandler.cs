using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FizzBuzz.Api.Services
{
    public interface IFizzBuzzResponseHandler
    {
        Task WriteResponse(HttpResponse response, IEnumerable<string> fizzBuzzSequence);
    }
}