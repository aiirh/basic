using System.Text;

namespace Aiirh.Basic.Utilities
{
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
    }
}
