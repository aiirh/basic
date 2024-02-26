using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Utilities;

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

    public static bool IsStandardType(this Type type)
    {
            return type.IsPrimitive ||
                   type == typeof(string) ||
                   type == typeof(decimal) ||
                   type == typeof(DateTime) ||
                   type == typeof(Guid) ||
                   type == typeof(TimeSpan) ||
                   type == typeof(Uri) ||
                   type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong) ||
                   type == typeof(float) ||
                   type == typeof(double) ||
                   type == typeof(char) ||
                   type == typeof(bool);
        }

    public static bool IsStandardType<T>()
    {
            var type = typeof(T);
            return type.IsStandardType();
        }
}