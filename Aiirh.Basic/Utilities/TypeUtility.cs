using System.Collections.Generic;

namespace Aiirh.Basic.Utilities
{
    public static class TypeUtility
    {
        public static bool IsNullOrDefault<T>(this T value)
        {
            if (value is string stringValue)
            {
                return string.IsNullOrWhiteSpace(stringValue);
            }

            var result = EqualityComparer<T>.Default.Equals(value, default);
            return result;
        }
    }
}
