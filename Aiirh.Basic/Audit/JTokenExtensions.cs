using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aiirh.Basic.Audit
{
    internal static class JTokenExtensions
    {
        private const string PathSeparator = "->";

        public static string RemovePathIndexer(this string pathWithIndex)
        {
            var removeIndexInBracketsRegex = new Regex(@"\[\d+\]", RegexOptions.Compiled);
            var removeDotRegex = new Regex(@"\.", RegexOptions.Compiled);
            var outputString = removeIndexInBracketsRegex.Replace(pathWithIndex, string.Empty);
            var result = removeDotRegex.Replace(outputString, PathSeparator);
            return result;
        }

        public static string PathConcat(this string path, string nextSegment)
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

            return $"{path}{PathSeparator}{nextSegment}";
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
}
