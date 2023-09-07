using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
    internal static class JsonUtility
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

        internal static JObject CreateJObjectWithProperty(string propertyName, JToken propertyValue)
        {
            var jsonObject = new JObject { { propertyName, propertyValue } };
            return jsonObject;
        }

        internal static string AddStringPropertyToJson(this string jsonString, string propertyName, string propertyValue)
        {
            // Deserialize the JSON string to a JObject
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            // Add the new property with the specified name and value
            jsonObject[propertyName] = propertyValue;

            // Serialize the updated JObject back to a JSON string
            string updatedJsonString = jsonObject.ToString();

            return updatedJsonString;
        }

        internal static void AddPropertyToJson(this JObject jsonObject, string propertyName, JToken propertyValue)
        {
            jsonObject[propertyName] = propertyValue;
        }

        public static string AddJsonToJson(string existingJson, string newJson)
        {
            // Deserialize the existing JSON into a JObject
            JObject existingObject = JObject.Parse(existingJson);

            // Merge or add the new JSON fragment into the existing JSON structure
            JObject newObject = JObject.Parse(newJson);
            existingObject.Merge(newObject, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            // Serialize the combined structure back into a JSON string
            string updatedJson = existingObject.ToString();

            return updatedJson;
        }
    }

    internal class AuditLogShort
    {
        public string PropertyName { get; set; }

        public string Value { get; set; }
    }
}
