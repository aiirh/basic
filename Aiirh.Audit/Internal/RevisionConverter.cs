using Aiirh.Basic.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Aiirh.Audit.Internal
{
    internal static class RevisionConverter
    {
        public static string ToRevisionJson<T>(this T obj)
        {
            var type = typeof(T);
            var jObject = JsonUtility.CreateJObjectWithProperty("RevisionType", new JValue(type.FullName));
            if (type.IsStandardType())
            {
                jObject.AddPropertyToJson("Value", new JValue(obj.ToString()));
                jObject.AddPropertyToJson("PropertyNamesMapping", JObject.FromObject(new Dictionary<string, string>()));
                return jObject.ToString(Formatting.None);
            }

            if (type.IsArray && type.GetElementType().IsStandardType())
            {
                jObject.AddPropertyToJson("Values", JArray.FromObject(obj));
                jObject.AddPropertyToJson("PropertyNamesMapping", JObject.FromObject(new Dictionary<string, string>()));
                return jObject.ToString(Formatting.None);
            }

            if (type.IsArray)
            {
                var attributes = type.GetElementType().GetPropertyAttributesDeep<AuditableAttribute>();
                if (attributes.IsNullOrEmpty())
                {
                    return null;
                }

                var displayNames = attributes.GetPropertyToDisplayNameMapping();
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new RevisionsContractResolver(attributes) });
                var mainData = JArray.FromObject(obj, serializer);
                jObject.AddPropertyToJson("Values", mainData);
                jObject.AddPropertyToJson("PropertyNamesMapping", JObject.FromObject(displayNames));
                return jObject.ToString(Formatting.None);
            }
            else
            {
                var attributes = type.GetPropertyAttributesDeep<AuditableAttribute>();
                if (attributes.IsNullOrEmpty())
                {
                    return null;
                }
                var displayNames = attributes.GetPropertyToDisplayNameMapping();
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new RevisionsContractResolver(attributes) });
                var mainData = JObject.FromObject(obj, serializer);
                jObject.AddJsonToJson(mainData);
                jObject.AddPropertyToJson("PropertyNamesMapping", JObject.FromObject(displayNames));
                return jObject.ToString(Formatting.None);
            }
        }

        private static IDictionary<string, string> GetPropertyToDisplayNameMapping(this IDictionary<PropertyInfo, AuditableAttribute> attributes)
        {
            return attributes.Where(x => x.Key.ReflectedType != null).Select(x =>
            {
                var propertyNameFromAttribute = x.Value.PropertyName;
                var displayNameFromAttribute = x.Value.DisplayName;
                var fullPropertyNameFromPropertyInfo = $"{x.Key.ReflectedType.FullName}.{x.Key.Name}";
                var propertyNameFromPropertyInfo = x.Key.Name;
                var finalPropertyNameForDisplay = string.IsNullOrWhiteSpace(propertyNameFromAttribute)
                    ? propertyNameFromPropertyInfo
                    : propertyNameFromAttribute;
                var key = string.IsNullOrWhiteSpace(propertyNameFromAttribute) ? fullPropertyNameFromPropertyInfo : propertyNameFromAttribute;
                var displayName = string.IsNullOrWhiteSpace(displayNameFromAttribute) ? finalPropertyNameForDisplay : displayNameFromAttribute;
                return new KeyValuePair<string, string>(key, displayName);
            }).DistinctBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }
    }

    internal class RevisionsContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<string, string> _attributesToCreate;

        public RevisionsContractResolver(Dictionary<PropertyInfo, AuditableAttribute> attributes)
        {
            _attributesToCreate = attributes
                .Where(x => x.Key.ReflectedType != null)
                .ToDictionary(x => $"{x.Key.ReflectedType.FullName}.{x.Key.Name}", x =>
                {
                    Debug.Assert(x.Key.ReflectedType != null, "x.Key.ReflectedType != null");
                    return string.IsNullOrWhiteSpace(x.Value.PropertyName)
                        ? $"{x.Key.ReflectedType.FullName}.{x.Key.Name}"
                        : x.Value.PropertyName;
                });
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (string.IsNullOrWhiteSpace(property.PropertyName) || property.DeclaringType == null)
            {
                property.ShouldSerialize = _ => false;
                return property;
            }

            if (!_attributesToCreate.TryGetValue($"{property.DeclaringType.FullName}.{property.PropertyName}", out var newName))
            {
                property.ShouldSerialize = _ => false;
                return property;
            }

            property.PropertyName = newName;
            return property;
        }
    }
}
