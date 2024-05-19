using Aiirh.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Aiirh.Basic.Utilities;

public static class EnumUtility
{
    public static bool HasDefinedValue(this Enum val)
    {
        var type = val.GetType();
        return Enum.IsDefined(type, val);
    }

    public static string ToString<T>(this T val, EnumToStringOption option) where T : Enum
    {
        switch (option)
        {
            case EnumToStringOption.UseNumericValue:
                var numericValue = Convert.ToInt64(val);
                return numericValue.ToString();
            case EnumToStringOption.UseName:
                return val.ToString();
            case EnumToStringOption.UseEnumMemberAttributeValue:
                return val.ToEnumMemberAttributeValue();
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }

    public static TDest CastEnum<TDest>(this Enum val) where TDest : Enum
    {
        var numericValue = Convert.ToInt32(val);
        return numericValue.ToEnum<TDest>();
    }

    public static bool TryCastEnum<TDest>(this Enum val, out TDest dest) where TDest : Enum
    {
        try
        {
            var numericValue = Convert.ToInt32(val);
            dest = numericValue.ToEnum<TDest>();
            return true;
        }
        catch
        {
            dest = default;
            return false;
        }
    }

    public static T ToEnum<T>(this int value) where T : Enum
    {
        var type = typeof(T);
        var commonErrorMessage = $@"Value ""{value}"" was not recognized as ""{type.Name}"" enum";
        try
        {
            var enumValue = (T)Enum.ToObject(type, value);
            return Enum.IsDefined(type, enumValue) ? enumValue : throw new SimpleException(commonErrorMessage);
        }
        catch (SimpleException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new SimpleException(commonErrorMessage, e);
        }
    }

    public static T ToEnum<T>(this string value) where T : Enum
    {
        var type = typeof(T);
        var commonErrorMessage = $@"Value ""{value}"" was not recognized as ""{type.Name}"" enum";
        try
        {
            var enumValue = (T)Enum.Parse(type, value, true);
            return Enum.IsDefined(type, enumValue) ? enumValue : throw new SimpleException(commonErrorMessage);
        }
        catch (SimpleException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new SimpleException(commonErrorMessage, e);
        }
    }

    public static IEnumerable<T> GetAllValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    private static string ToEnumMemberAttributeValue<T>(this T value) where T : Enum
    {
        var enumType = typeof(T);
        var name = Enum.GetName(enumType, value);
        var fieldInfo = enumType.GetField(name);
        if (fieldInfo == null)
        {
            return value.ToString();
        }

        var attribute = fieldInfo.GetCustomAttributes(typeof(EnumMemberAttribute), true).FirstOrDefault();
        if (!(attribute is EnumMemberAttribute enumMemberAttribute))
        {
            return value.ToString();
        }

        return enumMemberAttribute.Value;
    }
}

public enum EnumToStringOption
{
    UseNumericValue,
    UseName,
    UseEnumMemberAttributeValue
}