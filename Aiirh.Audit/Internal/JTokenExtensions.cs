using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aiirh.Audit.Internal;

internal static class JTokenExtensions
{
    public static string RemoveDotsAndPluses(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        var removeDotRegex = new Regex("[.+]", RegexOptions.Compiled);
        var result = removeDotRegex.Replace(str, string.Empty);
        return result;
    }

    public static string RemovePathIndexer(this string pathWithIndex, string pathSeparator)
    {
        var removeIndexInBracketsRegex = new Regex(@"\[\d+\]", RegexOptions.Compiled);
        var removeDotRegex = new Regex(@"\.", RegexOptions.Compiled);
        var outputString = removeIndexInBracketsRegex.Replace(pathWithIndex, string.Empty);
        var result = removeDotRegex.Replace(outputString, pathSeparator);
        return result;
    }

    public static string PathConcat(this string path, string nextSegment, string pathSeparator)
    {
        if (string.IsNullOrWhiteSpace(path) && string.IsNullOrWhiteSpace(nextSegment))
        {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(nextSegment))
        {
            return path;
        }

        if (string.IsNullOrWhiteSpace(path))
        {
            return nextSegment;
        }

        return $"{path}{pathSeparator}{nextSegment}";
    }

    public static IEnumerable<AuditLogShort> ToAuditLogsShort(this JToken added)
    {
        switch (added.Type)
        {
            case JTokenType.Object:
            case JTokenType.Array:
                foreach (var subToken in added)
                {
                    foreach (var entry in subToken.ToAuditLogsShort())
                    {
                        yield return entry;
                    }
                }
                yield break;

            default:
                switch (added)
                {
                    case JProperty property:
                        yield return new AuditLogShort
                        {
                            PropertyName = property.Name,
                            Value = property.Value.ToString()
                        };
                        yield break;
                    default:
                        var value = (JValue)added;
                        yield return new AuditLogShort
                        {
                            PropertyName = null,
                            Value = value.Value?.ToString()
                        };
                        yield break;
                }
        }
    }
}

internal class AuditLogShort
{
    public string PropertyName { get; set; }

    public string Value { get; set; }
}