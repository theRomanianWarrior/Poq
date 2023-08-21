using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoqAssignment.Domain.Helpers
{
    public class EnumMemberValueConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException($"Unexpected token type: {reader.TokenType}");

            var enumMemberValue = reader.GetString();
            var enumValues = Enum.GetValues(typeToConvert);

            foreach (var enumValue in enumValues)
            {
                var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                var enumMemberAttribute =
                    fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false) as EnumMemberAttribute[];

                if (enumMemberAttribute != null && enumMemberAttribute.Length > 0 &&
                    enumMemberAttribute[0].Value == enumMemberValue) return (T) enumValue;
            }

            throw new JsonException($"No matching enum value found for {enumMemberValue}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value is Enum enumValue)
            {
                var enumMemberValue = enumValue.GetEnumMemberValue();
                writer.WriteStringValue(enumMemberValue);
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}