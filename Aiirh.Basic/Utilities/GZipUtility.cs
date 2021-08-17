using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Aiirh.Basic.Utilities
{
    public static class GZipUtility
    {
        public static byte[] _gZipHeaderBytes = { 0x1f, 0x8b };

        public static bool IsPossiblyGZippedBytes(byte[] a)
        {
            if (a == null || a.Length < 11)
            {
                return false;
            }

            return !_gZipHeaderBytes.Where((t, i) => t != a[i]).Any();
        }

        public static bool IsCompressNeeded(this byte[] data)
        {
            return data != null && data.Length > 360 && !IsPossiblyGZippedBytes(data);
        }

        public static async Task<byte[]> CompressAsync(this byte[] data)
        {
            if (!IsCompressNeeded(data))
            {
                return data;
            }
            var newData = new MemoryStream();
            using var stream = new GZipStream(newData, CompressionMode.Compress);
            await stream.WriteAsync(data, 0, data.Length);
            return newData.ToArray();
        }

        public static async Task<byte[]> DecompressIfGZipAsync(this byte[] data)
        {
            if (data == null || data.Length <= 10 || !IsPossiblyGZippedBytes(data))
            {
                return data;
            }
            var oldData = new MemoryStream(data);
            var newData = new MemoryStream();
            using var stream = new GZipStream(oldData, CompressionMode.Decompress);
            await stream.CopyToAsync(newData);
            return newData.ToArray();
        }
    }
}
