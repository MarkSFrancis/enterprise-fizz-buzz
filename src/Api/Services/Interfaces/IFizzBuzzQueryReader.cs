using FizzBuzz.Api.Models;
using Microsoft.AspNetCore.Http;

namespace FizzBuzz.Api.Services
{
    public interface IFizzBuzzQueryReader
    {
        FizzBuzzSettingsModel GetSettings(IQueryCollection query);
    }
}