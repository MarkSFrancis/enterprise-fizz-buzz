using Newtonsoft.Json;

namespace FizzBuzz.Api.Services
{
    public class JsonSerializerService : IJsonSerializerService
    {
        public string Serialize(object o)
        {
            var serialized = JsonConvert.SerializeObject(o);

            return serialized;
        }
    }
}
