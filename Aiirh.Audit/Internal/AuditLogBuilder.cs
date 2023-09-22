using Aiirh.Basic.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Audit.Internal
{
    internal static class AuditLogBuilder
    {
        public static IAuditLog Build(string oldJson, string newJson, DateTime createdDate, string author, string comment, string pathSeparator)
        {
            var oldObject = JsonConvert.DeserializeObject<JObject>(oldJson);
            var newObject = JsonConvert.DeserializeObject<JObject>(newJson);
            var propertyNamesMappingFromOld = oldObject.GetJsonPropertyByName("PropertyNamesMapping")?.ToString(Formatting.None).Convert<Dictionary<string, string>>() ?? new Dictionary<string, string>();
            var propertyNamesMappingFromNew = newObject.GetJsonPropertyByName("PropertyNamesMapping")?.ToString(Formatting.None).Convert<Dictionary<string, string>>() ?? new Dictionary<string, string>();
            var propertyNamesMapping = propertyNamesMappingFromOld.Merge(propertyNamesMappingFromNew);

            oldObject.RemoveJsonPropertyByName("PropertyNamesMapping");
            oldObject.RemoveJsonPropertyByName("RevisionType");

            newObject.RemoveJsonPropertyByName("PropertyNamesMapping");
            newObject.RemoveJsonPropertyByName("RevisionType");

            return CompareJsonObjects(oldObject, newObject, createdDate, author, propertyNamesMapping, comment, pathSeparator);
        }

        private static AuditLog CompareJsonObjects(JToken oldToken, JToken newToken, DateTime createdDate, string author, IDictionary<string, string> propertyNamesMapping, string comment, string pathSeparator)
        {
            var auditLog = new AuditLog(createdDate, author);
            if (oldToken == null || newToken == null)
            {
                return auditLog;
            }

            if (oldToken.Type != newToken.Type)
            {
                // Type has changed, add to the audit log
                auditLog.AddEntry(AuditLogEntry.Edit(oldToken.Path.RemovePathIndexer(pathSeparator), newToken.ToString(), oldToken.ToString(), propertyNamesMapping, pathSeparator, comment));
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

                            var subAuditLog = CompareJsonObjects(oldValue, newValue, createdDate, author, propertyNamesMapping, comment, pathSeparator);
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
                                var subAuditLog = CompareJsonObjects(oldArray[i], newArray[i], createdDate, author, propertyNamesMapping, comment, pathSeparator);
                                auditLog.AddEntriesFromAnotherAuditLog(subAuditLog);
                            }
                            else if (i < oldArray.Count)
                            {
                                var oldObject = oldArray[i];
                                var oldAuditLogEntries = oldObject.ToAuditLogsShort().Select(x => AuditLogEntry.Remove(oldArray.Path.RemovePathIndexer(pathSeparator).PathConcat(x.PropertyName, pathSeparator), x.Value, propertyNamesMapping, pathSeparator, comment));
                                auditLog.AddEntries(oldAuditLogEntries);
                            }
                            else
                            {
                                var newObject = newArray[i];
                                var newAuditLogEntries = newObject.ToAuditLogsShort().Select(x => AuditLogEntry.Add(newArray.Path.RemovePathIndexer(pathSeparator).PathConcat(x.PropertyName, pathSeparator), x.Value, propertyNamesMapping, pathSeparator, comment));
                                auditLog.AddEntries(newAuditLogEntries);
                            }
                        }

                        break;
                    default:
                        if (!JToken.DeepEquals(oldToken, newToken))
                        {
                            auditLog.AddEntry(AuditLogEntry.Edit(oldToken.Path.RemovePathIndexer(pathSeparator), newToken.ToString(), oldToken.ToString(), propertyNamesMapping, pathSeparator, comment));
                        }

                        break;
                }
            }

            return auditLog;
        }

        private static void RemoveSameElementsFromArrays(JArray array1, JArray array2)
        {
            var removedElements1 = new HashSet<JToken>();
            var removedElements2 = new HashSet<JToken>();

            for (int i = array1.Count - 1; i >= 0; i--)
            {
                var token1 = array1[i];

                for (int j = 0; j < array2.Count; j++)
                {
                    var token2 = array2[j];
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
    }
}
