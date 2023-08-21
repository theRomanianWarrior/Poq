using Newtonsoft.Json;
using PoqAssignment.Application.Helpers;
using PoqAssignment.Domain.Contracts;

namespace PoqAssignment.Application.Services
{
    public class SerializationService : ISerializationService
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public SerializationService()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            _serializerSettings.Converters.Add(new MaskedJsonConverter());
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, _serializerSettings);
        }
    }
}