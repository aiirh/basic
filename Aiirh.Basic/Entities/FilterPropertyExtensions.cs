using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Entities;

public static class FilterPropertyExtensions
{
    public static bool IsFilterRequired<T>(this FilterProperty<T> property)
    {
        if (property == null)
        {
            return false;
        }

        if (property.EmptyFilterValueBehavior == EmptyFilterValueBehavior.Filter)
        {
            return true;
        }

        if (property.Value == null)
        {
            return false;
        }

        return true;
    }

    public static FilterProperty<T> ToFilterProperty<T>(this T value)
    {
        return new FilterProperty<T>(value);
    }

    public static FilterProperty<string> ToFilterPropertyIfNotEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : new FilterProperty<string>(value);
    }

    public static FilterProperty<T> ToFilterPropertyIfHasValue<T>(this T? value) where T : struct, Enum
    {
        return !value.HasValue ? null : new FilterProperty<T>(value.Value);
    }

    public static FilterProperty<T[]> ToFilterPropertyIfContainsAny<T>(this T[] value)
    {
        return value.IsNullOrEmpty() ? null : new FilterProperty<T[]>(value);
    }

    public static FilterProperty<T[]> ToFilterPropertyIfContainsAny<T>(this IEnumerable<T> value)
    {
        return value?.ToArray().ToFilterPropertyIfContainsAny();
    }

    public static FilterProperty<T[]> ToFilterPropertyWithFilterBehaviour<T>(this T[] value)
    {
        return new FilterProperty<T[]>(value, EmptyFilterValueBehavior.Filter);
    }
}