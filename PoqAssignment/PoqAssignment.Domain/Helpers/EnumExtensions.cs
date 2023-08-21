using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace PoqAssignment.Domain.Helpers
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            if (fieldInfo == null) return null;

            var attribute = fieldInfo.GetCustomAttribute<EnumMemberAttribute>();

            return attribute?.Value ?? value.ToString();
        }
    }
}