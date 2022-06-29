using Aiirh.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Utilities
{
    public static class EnumUtility
    {
        public static bool HasDefinedValue(this Enum val)
        {
            var type = val.GetType();
            return Enum.IsDefined(type, val);
        }

        public static string ToString(this Enum val, EnumToStringOption option)
        {
            switch (option)
            {
                case EnumToStringOption.UseNumericValue:
                    var numericValue = Convert.ToInt64(val);
                    return numericValue.ToString();
                case EnumToStringOption.UseName:
                    return val.ToString();
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
    }

    public enum EnumToStringOption
    {
        UseNumericValue,
        UseName
    }
}
