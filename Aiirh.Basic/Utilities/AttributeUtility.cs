using System;
using System.Collections;
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

        public static Dictionary<PropertyInfo, TAttribute> GetPropertyAttributes<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var result = new Dictionary<PropertyInfo, TAttribute>();
            if (type == null)
            {
                return null;
            }

            var properties = type.GetProperties();
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

        public static Dictionary<PropertyInfo, TAttribute> GetPropertyAttributesDeep<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var result = new Dictionary<PropertyInfo, TAttribute>();
            if (type == null)
            {
                return result;
            }

            GetPropertyAttributesRecursive(type, result);
            return result;
        }

        private static void GetPropertyAttributesRecursive<TAttribute>(Type objType, Dictionary<PropertyInfo, TAttribute> result) where TAttribute : Attribute
        {
            var properties = objType.GetProperties();
            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttributes(true).OfType<TAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    result.TryAdd(prop, attr);
                }

                // Check if the property is a reference type and not a primitive type or a string
                if (!prop.PropertyType.IsStandardType())
                {
                    // If it's a collection type, get the element type and check attributes on its properties
                    if (typeof(IEnumerable).IsAssignableFrom(prop.PropertyType))
                    {
                        var elementType = prop.PropertyType.GetElementType() ?? prop.PropertyType.GetGenericArguments().FirstOrDefault();
                        if (elementType != null)
                        {
                            GetPropertyAttributesRecursive(elementType, result);
                        }
                    }
                    else
                    {
                        // Recursively check attributes on properties of this property
                        GetPropertyAttributesRecursive(prop.PropertyType, result);
                    }
                }
            }
        }
    }
}
