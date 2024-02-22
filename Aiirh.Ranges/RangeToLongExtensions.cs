using System;
using System.Globalization;

namespace Aiirh.Ranges;

internal static class RangeToLongExtensions
{
    public static long GetLongRepresentation(this object value)
    {
        return value switch
        {
            DateTime dataTime => dataTime.Ticks,
            int integer => integer,
            _ => throw new ArgumentException("Type is not supported")
        };
    }

    public static T GetValueFromLong<T>(this long value)
    {
        if (typeof(T) == typeof(DateTime))
        {
            var date = new DateTime(value, DateTimeKind.Unspecified);
            return (T)Convert.ChangeType(date, typeof(T), CultureInfo.InvariantCulture);
        }

        if (typeof(T) == typeof(int))
        {
            var integer = (int)value;
            return (T)Convert.ChangeType(integer, typeof(T), CultureInfo.InvariantCulture);
        }

        throw new ArgumentException("Type is not supported");
    }
}