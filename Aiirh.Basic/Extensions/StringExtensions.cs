namespace Aiirh.Basic.Extensions
{
    public static class StringExtensions
    {
        public static bool HasValue(this string str)
        {
            str = str?.Trim();
            return !string.IsNullOrEmpty(str);
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
    }
}
