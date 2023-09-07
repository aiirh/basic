using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aiirh.Basic.Audit
{
    internal static class AuditLogBuilder
    {
        public static IAuditLog Build(string json1, string json2)
        {
            var oldObject = JsonConvert.DeserializeObject<JObject>(json1);
            var newObject = JsonConvert.DeserializeObject<JObject>(json2);
            return CompareJsonObjects(oldObject, newObject);
        }

        static AuditLog CompareJsonObjects(JToken oldToken, JToken newToken)
        {
            var auditLog = new AuditLog();
            if (oldToken == null || newToken == null)
            {
                return auditLog;
            }

            if (oldToken.Type != newToken.Type)
            {
                // Type has changed, add to the audit log
                auditLog.AddEntry(new AuditLogEntry
                {
                    PropertyName = oldToken.Path,
                    OldValue = oldToken.ToString(),
                    NewValue = newToken.ToString()
                });
            }
            else
            {
                switch (oldToken.Type)
                {
                    case JTokenType.Object:
                        var oldObj = (JObject)oldToken;
                        var newObj = (JObject)newToken;

                        foreach (var property in oldObj.Properties())
                        {
                            var propertyName = property.Name;
                            var oldValue = property.Value;
                            var newValue = newObj[propertyName];

                            var subAuditLog = CompareJsonObjects(oldValue, newValue);
                            auditLog.AddEntriesFromAnotherAuditLog(subAuditLog);
                        }

                        break;
                    case JTokenType.Array:
                        var oldArray = (JArray)oldToken;
                        var newArray = (JArray)newToken;

                        RemoveSameElementsFromArrays(oldArray, newArray);

                        for (int i = 0; i < oldArray.Count || i < newArray.Count; i++)
                        {
                            if (i < oldArray.Count && i < newArray.Count)
                            {
                                var subAuditLog = CompareJsonObjects(oldArray[i], newArray[i]);
                                auditLog.AddEntriesFromAnotherAuditLog(subAuditLog);
                            }
                            else if (i < oldArray.Count)
                            {
                                var oldObject = oldArray[i];
                                var oldAuditLogEntries = oldObject.ToAuditLogsShort().Select(x => new AuditLogEntry
                                {
                                    PropertyName = $"{oldArray.Path.RemoveIndexer()}->{x.PropertyName}",
                                    NewValue = null,
                                    OldValue = x.Value
                                });
                                auditLog.AddEntries(oldAuditLogEntries);
                            }
                            else
                            {
                                var newObject = newArray[i];
                                var newAuditLogEntries = newObject.ToAuditLogsShort().Select(x => new AuditLogEntry
                                {
                                    PropertyName = $"{newArray.Path.RemoveIndexer()}->{x.PropertyName}",
                                    NewValue = x.Value,
                                    OldValue = null
                                });
                                auditLog.AddEntries(newAuditLogEntries);

                                ////auditLog.AddEntry(new AuditLogEntry
                                ////{
                                ////    PropertyName = newArray.Path.RemoveIndexer(),
                                ////    OldValue = null,
                                ////    NewValue = newArray[i].ToString()
                                ////});
                            }
                        }

                        break;
                    default:
                        if (!JToken.DeepEquals(oldToken, newToken))
                        {
                            auditLog.AddEntry(new AuditLogEntry
                            {
                                PropertyName = oldToken.Path.RemoveIndexer(),
                                OldValue = oldToken.ToString(),
                                NewValue = newToken.ToString()
                            });
                        }
                        break;
                }
            }

            return auditLog;
        }

        private static void RemoveSameElementsFromArrays(JArray array1, JArray array2)
        {
            HashSet<JToken> removedElements1 = new HashSet<JToken>();
            HashSet<JToken> removedElements2 = new HashSet<JToken>();

            for (int i = array1.Count - 1; i >= 0; i--)
            {
                JToken token1 = array1[i];

                for (int j = 0; j < array2.Count; j++)
                {
                    JToken token2 = array2[j];
                    if (!removedElements1.Contains(token1) && !removedElements2.Contains(token2) && JToken.DeepEquals(token1, token2))
                    {
                        array1.RemoveAt(i);
                        array2.RemoveAt(j);
                        removedElements1.Add(token1);
                        removedElements2.Add(token2);
                        break;
                    }
                }
            }
        }

        private static string RemoveIndexer(this string pathWithIndex)
        {
            string pattern1 = @"\[\d+\]\.";
            var pattern2 = @"\[\d+\]";
            string replacement1 = "->";
            string replacement2 = "";

            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);
            string outputString = regex1.Replace(pathWithIndex, replacement1);
            return regex2.Replace(outputString, replacement2);
        }
    }
}
