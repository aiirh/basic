using System;
using System.Text.RegularExpressions;

namespace Aiirh.Basic.Utilities;

public static class StringUtility
{
    [Obsolete("Use IsNotNullOrWhiteSpace instead", true)]
    public static bool HasValue(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsNotNullOrWhiteSpace(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    public static bool ContainsCaseInsensitive(this string str, string strToCompare)
    {
#if NETSTANDARD
        return str.ToLower().Contains(strToCompare.ToLower());
#else
        return str.Contains(strToCompare, StringComparison.InvariantCultureIgnoreCase);
#endif
    }

    public static string Truncate(this string str, int maxLength, string truncatedSuffix = "...")
    {
        if (str.IsNullOrWhiteSpace() || str.Length <= maxLength)
        {
            return str;
        }

        truncatedSuffix ??= string.Empty;

#if NETSTANDARD
        return str.Substring(0, maxLength - truncatedSuffix.Length) + truncatedSuffix;
#else
        return str[..(maxLength - truncatedSuffix.Length)] + truncatedSuffix;
#endif
    }

    /// <summary>
    /// Returns a copy of this string converted to sentence case.
    /// </summary>
    /// <param name="sourceString">Source string to convert.</param>
    /// <returns>A string in sentence case.</returns>
    public static string ToSentence(this string sourceString)
    {
        var lowerCase = sourceString.ToLower();
        var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        return r.Replace(lowerCase, s => s.Value.ToUpper());
    }
}