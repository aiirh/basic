using System;
using System.Globalization;

namespace Aiirh.Basic.Utilities;

public static class MemorySizeUtility
{
    private static readonly string[] BinarySizes = ["B", "KiB", "MiB", "GiB", "TiB"];
    private static readonly string[] DecimalSizes = ["B", "KB", "MB", "GB", "TB"];

    /// <summary>
    /// Returns memory size with units.
    /// </summary>
    /// <param name="sizeInBytes">Integer value that represents memory size in bytes.</param>
    /// <param name="sizeUnits">Memory size units (decimal or binary).</param>
    /// <returns>Memory size with units, e.g. "41.34 KB"</returns>
    public static string ToMemorySizeString(this int sizeInBytes, MemorySizeUnits sizeUnits = MemorySizeUnits.Decimal)
    {
        return ((long)sizeInBytes).ToMemorySizeString(sizeUnits);
    }

    public static string ToMemorySizeString(this long sizeInBytes, MemorySizeUnits sizeUnits = MemorySizeUnits.Decimal)
    {
        var sizes = sizeUnits == MemorySizeUnits.Decimal ? DecimalSizes : BinarySizes;
        var baseValue = sizeUnits == MemorySizeUnits.Decimal ? 1000D : 1024D;
        var len = Convert.ToDouble(sizeInBytes);
        var order = 0;
        while (len >= baseValue * .9 && order < sizes.Length - 1)
        {
            order++;
            len /= baseValue;
        }

        return string.Format(CultureInfo.CurrentCulture, "{0:0.##} {1}", len, sizes[order]);
    }
}

public enum MemorySizeUnits
{
    /// <summary>
    /// Shows size with base 2 (KiB, MiB, etc.)
    /// </summary>
    Binary,

    /// <summary>
    /// Shows size with base 10 (KB, MB, etc.)
    /// </summary>
    Decimal
}