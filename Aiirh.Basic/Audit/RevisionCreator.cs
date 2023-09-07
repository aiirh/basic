using Aiirh.Basic.Utilities;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Audit
{
    internal static class RevisionCreator
    {
        public static string CreateRevision<T>(T obj)
        {
            var type = typeof(T);
            var attributes = type.GetPropertyAttributes<AuditableAttribute>();
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
            _attributesToCreate = attributes.ToDictionary(x => x.Key.Name, x => x.Value.PropertyName);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (string.IsNullOrWhiteSpace(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
                return property;
            }

            if (!_attributesToCreate.TryGetValue(property.PropertyName, out var newName))
            {
                property.ShouldSerialize = _ => false;
                return property;
            }

            property.PropertyName = newName;
            return property;
        }
    }
}
