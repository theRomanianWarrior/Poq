using System.Runtime.Serialization;

namespace PoqAssignment.Domain.Enums
{
    public enum Highlight
    {
        [EnumMember(Value = "red")] Red,
        [EnumMember(Value = "green")] Green,
        [EnumMember(Value = "blue")] Blue
    }
}