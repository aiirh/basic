using Aiirh.Basic.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Audit
{
    internal static class RevisionCreator
    {
        public static string CreateRevision<T>(T obj)
        {
            var type = typeof(T);
            var attributes = type.GetPropertyAttributesDeep<AuditableAttribute>();
            if (attributes.IsNullOrEmpty())
            {
                return null;
            }

            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new RevisionsContractResolver(attributes) });
        }
    }

    public class RevisionsContractResolver : DefaultContractResolver
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
