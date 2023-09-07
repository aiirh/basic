using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
    internal static class JTokenConverter
    {
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
                    var property = (JProperty)added;
                    yield return new AuditLogShort
                    {
                        PropertyName = property.Name,
                        Value = property.Value.ToString()
                    };
                    yield break;
            }
        }
    }

    internal class AuditLogShort
    {
        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}
