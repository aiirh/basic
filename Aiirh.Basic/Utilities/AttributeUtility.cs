using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aiirh.Basic.Utilities
{
    public static class AttributeUtility
    {
        public static TAttribute GetAttributeValue<TAttribute>(this Type obj) where TAttribute : Attribute
        {
            return obj?.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();
        }

        public static Dictionary<PropertyInfo, TAttribute> GetPropertyAttributes<TAttribute>(this Type obj) where TAttribute : Attribute
        {
            var result = new Dictionary<PropertyInfo, TAttribute>();
            if (obj == null)
            {
                return null;
            }

            var properties = obj.GetProperties();
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();
                if (attr == null)
                {
                    continue;
                }

                result.Add(prop, attr);
            }

            return result;
        }
    }
}
