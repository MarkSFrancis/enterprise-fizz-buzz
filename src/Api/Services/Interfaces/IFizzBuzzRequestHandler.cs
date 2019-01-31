using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace FizzBuzz.Api.Services
{
    public interface IFizzBuzzRequestHandler
    {
        IEnumerable<string> HandleRequest(HttpRequest request);
    }
}