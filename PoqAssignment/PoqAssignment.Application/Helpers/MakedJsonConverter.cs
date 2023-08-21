using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PoqAssignment.Application.Helpers
{
    public class MaskedJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Guid);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException(); // Not needed for serialization
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Guid stringValue)
            {
                // Mask sensitive GUIDs
                var maskedValue = MaskGuid(stringValue.ToString());
                writer.WriteValue(maskedValue);
            }
            else
            {
                JToken.FromObject(value).WriteTo(writer);
            }
        }

        private static string MaskGuid(string guid)
        {
            if (Guid.TryParse(guid, out var parsedGuid))
            {
                var maskedGuid = parsedGuid.ToString("N"); // Remove hyphens

                if (maskedGuid.Length >= 4)
                    maskedGuid = maskedGuid.Substring(0, 2) + new string('*', maskedGuid.Length - 4) +
                                 maskedGuid.Substring(maskedGuid.Length - 2);

                return maskedGuid;
            }

            return guid; // Return unchanged if not a valid GUID
        }
    }
}