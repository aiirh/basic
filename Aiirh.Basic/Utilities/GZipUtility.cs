using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Aiirh.Basic.Utilities;

public static class GZipUtility
{
    private static readonly byte[] GZipHeaderBytes = [0x1f, 0x8b];

    [Obsolete("Don't use this method. Consider using GZipCompress instead")]
    public static async Task<byte[]> CompressAsync(this byte[] data)
    {
            if (!IsCompressNeeded(data))
            {
                return data;
            }

            using var newData = new MemoryStream();
            using var stream = new GZipStream(newData, CompressionMode.Compress);
            await stream.WriteAsync(data, 0, data.Length);
            return newData.ToArray();
        }

    public static byte[] GZipCompress(this byte[] data)
    {
            using var uncompressed = new MemoryStream(data);
            using var compressed = new MemoryStream();
            using (var compressor = new GZipStream(compressed, CompressionMode.Compress))
            {
                uncompressed.CopyTo(compressor);
            }

            return compressed.ToArray();
        }

    public static byte[] GZipDecompress(this byte[] data)
    {
            using var compressed = new MemoryStream(data);
            using var decompressed = new MemoryStream();
            using var compressor = new GZipStream(compressed, CompressionMode.Decompress);
            compressor.CopyTo(decompressed);
            return decompressed.ToArray();
        }

    [Obsolete("Don't use this method. Consider using DecompressIfGZipAsync instead")]
    public static async Task<byte[]> DecompressIfGZipAsync(this byte[] data)
    {
            if (data == null || data.Length <= 10 || !IsPossiblyGZippedBytes(data))
            {
                return data;
            }

            using var oldData = new MemoryStream(data);
            using var newData = new MemoryStream();
            using var stream = new GZipStream(oldData, CompressionMode.Decompress);
            await stream.CopyToAsync(newData);
            return newData.ToArray();
        }

    private static bool IsCompressNeeded(this IReadOnlyList<byte> data)
    {
            return data != null && data.Count > 360 && !IsPossiblyGZippedBytes(data);
        }

    private static bool IsPossiblyGZippedBytes(IReadOnlyList<byte> a)
    {
            if (a == null || a.Count < 11)
            {
                return false;
            }

            return !GZipHeaderBytes.Where((t, i) => t != a[i]).Any();
        }
}