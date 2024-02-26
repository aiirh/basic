using System.Text;

namespace Aiirh.Basic.Utilities;

public static class ByteArrayConverter
{
    public static byte[] ToByteArray<T>(this T obj)
    {
            var json = obj.Convert();
            return json.ToByteArray();
        }

    public static byte[] ToByteArray(this string value)
    {
            var byteArray = !string.IsNullOrWhiteSpace(value) ? Encoding.UTF8.GetBytes(value) : null;
            return byteArray;
        }

    public static string FromByteArray(this byte[] bytes)
    {
            return Encoding.UTF8.GetString(bytes);
        }

    public static T FromByteArray<T>(this byte[] bytes)
    {
            var str = bytes.FromByteArray();
            return str.Convert<T>();
        }
}