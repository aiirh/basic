using System.IO;
using System.IO.Compression;

namespace Aiirh.Basic.Utilities;

public static class GZipUtility
{
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
}