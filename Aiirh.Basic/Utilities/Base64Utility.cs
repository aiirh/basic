using System;
using System.Text;

namespace Aiirh.Basic.Utilities
{
    public static class Base64Utility
    {
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Encode<T>(this T obj)
        {
            var json = obj.Convert();
            var plainTextBytes = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static T Base64Decode<T>(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            var json = Encoding.UTF8.GetString(base64EncodedBytes);
            return json.Convert<T>();
        }
    }
}
