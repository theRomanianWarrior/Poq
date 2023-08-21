using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using PoqAssignment.Domain.Helpers;

namespace PoqAssignment.Domain.Enums
{
    [JsonConverter(typeof(EnumMemberValueConverter<Size>))]
    public enum Size
    {
        [EnumMember(Value = "small")] Small,
        [EnumMember(Value = "medium")] Medium,
        [EnumMember(Value = "large")] Large
    }
}