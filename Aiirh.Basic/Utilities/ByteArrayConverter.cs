using System.Text;

namespace Aiirh.Basic.Utilities
{
    public static class ByteArrayConverter
    {
        public static byte[] ToByteArray(this string value)
        {
            byte[] byteArray;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var valueBytes = Encoding.UTF8.GetBytes(value);
                byteArray = valueBytes.CompressAsync().GetAwaiter().GetResult();
            }
            else
            {
                byteArray = null;
            }

            return byteArray;
        }
    }
}
