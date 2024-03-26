using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Aiirh.Basic.Utilities;

public static class TypeConverterUtility
{
    public static T Convert<T>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        if (typeof(T).IsEnum)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        if (typeof(T) == typeof(string) || typeof(T).IsValueType)
        {
            return (T)System.Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        return JsonConvert.DeserializeObject<T>(value);
    }

#if NETSTANDARD2_1 || NET6
    public static object Convert(this string value, Type type)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        if (type.IsEnum)
        {
            return Enum.Parse(type, value);
        }

        if (type == typeof(string) || type.IsValueType)
        {
            return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        return JsonConvert.DeserializeObject(value, type);
    }
#endif

    public static string Convert<T>(this T value)
    {
        if (value.IsNullOrDefault())
        {
            return default(T)?.ToString();
        }

        if (value is string || typeof(T).IsValueType)
        {
            return value.ToString();
        }

        return JsonConvert.SerializeObject(value);
    }



    public static T Convert<T>(this object value)
    {
        if (value == null)
        {
            return default;
        }
        var serializeObject = JsonConvert.SerializeObject(value);
        var settings = new JsonSerializerSettings
        {
            EqualityComparer = StringComparer.OrdinalIgnoreCase
        };
        return JsonConvert.DeserializeObject<T>(serializeObject, settings);
    }
}