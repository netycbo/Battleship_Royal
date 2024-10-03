using Battleship_Royal.Api.Handlers.Services.Interfaces;
using Newtonsoft.Json;

namespace Battleship_Royal.Api.Handlers.Services
{
    public class DeserializeService : IDeserializeService
    {
        public T Deserialize<T>(string json)
        {
            var serializer = new JsonSerializer();
            using (var stringReader = new StringReader(json))
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                return serializer.Deserialize<T>(jsonReader);
            }
        }
    }
}
