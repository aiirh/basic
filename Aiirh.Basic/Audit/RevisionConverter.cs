using Aiirh.Basic.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Audit
{
    internal static class RevisionConverter
    {
        public static string ToRevisionJson<T>(this T obj)
        {
            var type = typeof(T);
            var jObject = JsonUtility.CreateJObjectWithProperty("Type", new JValue(type.FullName));
            if (type.IsStandardType())
            {
                jObject.AddPropertyToJson("Value", new JValue(obj.ToString()));
                return jObject.ToString(Formatting.None);
            }

            if (type.IsArray && type.GetElementType().IsStandardType())
            {
                jObject.AddPropertyToJson("Values", JArray.FromObject(obj));
                return jObject.ToString(Formatting.None);
            }

            if (type.IsArray)
            {
                var attributes = type.GetElementType().GetPropertyAttributesDeep<AuditableAttribute>();
                if (attributes.IsNullOrEmpty())
                {
                    return null;
                }
                var serializer = JsonSerializer.Create(new JsonSerializerSettings { ContractResolver = new RevisionsContractResolver(attributes) });
                var mainData = JArray.FromObject(obj, serializer);
                jObject.AddPropertyToJson("Values", mainData);
                return jObject.ToString(Formatting.None);
            }
            else
            {
                var attributes = type.GetPropertyAttributesDeep<AuditableAttribute>();
                if (attributes.IsNullOrEmpty())
                {
                    return null;
                }
                var jsonString = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new RevisionsContractResolver(attributes) });
                return jsonString;
            }
        }
    }

    internal class RevisionsContractResolver : DefaultContractResolver
    {
        private readonly Dictionary<string, string> _attributesToCreate;

        public RevisionsContractResolver(Dictionary<PropertyInfo, AuditableAttribute> attributes)
        {
            _attributesToCreate = attributes.Where(x => x.Key.ReflectedType != null).ToDictionary(x => $"{x.Key.ReflectedType.FullName}.{x.Key.Name}", x => x.Value.PropertyName);
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
