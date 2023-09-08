﻿using Newtonsoft.Json.Linq;
using System;

namespace Aiirh.Basic.Utilities
{
    public static class JsonUtility
    {
        public static JObject CreateJObjectWithProperty(string propertyName, JToken propertyValue)
        {
            var jsonObject = new JObject { { propertyName, propertyValue } };
            return jsonObject;
        }

        public static void AddPropertyToJson(this JObject jsonObject, string propertyName, JToken propertyValue)
        {
            jsonObject[propertyName] = propertyValue;
        }

        public static JObject AddJsonToJson(this JObject existingObject, JObject newObject)
        {
            existingObject.Merge(newObject, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            return existingObject;
        }

        public static JToken GetJsonPropertyByName(this string jsonString, string propertyName)
        {
            var jsonObject = JObject.Parse(jsonString);
            return jsonObject.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out JToken propertyValue) ? propertyValue : null;
        }

        public static bool EqualsAsJson(this string one, string other)
        {
            var obj1 = JObject.Parse(one);
            var obj2 = JObject.Parse(other);
            return JToken.DeepEquals(obj1, obj2);
        }
    }
}
