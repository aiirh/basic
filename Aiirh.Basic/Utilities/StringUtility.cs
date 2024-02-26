using System.Text.RegularExpressions;

namespace Aiirh.Basic.Utilities;

public static class StringUtility
{
    public static bool HasValue(this string str)
    {
        str = str?.Trim();
        return !string.IsNullOrEmpty(str);
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
        return str.ToLower().Contains(strToCompare.ToLower());
    }

    public static string Truncate(this string str, int maxLength, string truncatedSuffix = "...")
    {
        if (!str.HasValue() || str.Length <= maxLength)
        {
            return str;
        }
        if (truncatedSuffix == null)
        {
            truncatedSuffix = string.Empty;
        }
        return str.Substring(0, maxLength - truncatedSuffix.Length) + truncatedSuffix;
    }

    /// <summary>
    /// Returns a copy of this string converted to sentence case.
    /// </summary>
    /// <param name="sourceString">Source string to convert</param>
    /// <returns>A string in sentence case</returns>
    public static string ToSentence(this string sourceString)
    {
        var lowerCase = sourceString.ToLower();
        var r = new Regex(@"(^[a-z])|\.\s+(.)", RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        return r.Replace(lowerCase, s => s.Value.ToUpper());
    }
}