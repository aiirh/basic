﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aiirh.Basic.Utilities;

public static class DeepCopyHelper
{
    private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

    public static object DeepCopy(this object originalObject)
    {
        return InternalCopy(originalObject, new Dictionary<object, object>(new ReferenceEqualityComparer()));
    }

    public static T DeepCopy<T>(this T original)
    {
        return (T)DeepCopy((object)original);
    }

    /// <summary>
    /// Copies the properties from one object to another of the same class while preserving the reference to the target object.
    /// </summary>
    /// <typeparam name="T">The type of objects to copy.</typeparam>
    /// <param name="target">The target object to which properties will be copied.</param>
    /// <param name="source">The source object from which properties will be copied.</param>
    public static void SetPropertiesFrom<T>(this T target, T source)
    {
        if (target is null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var type = typeof(T);
        foreach (var propertyInfo in type.GetProperties())
        {
            if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
            {
                continue;
            }

            var value = propertyInfo.GetValue(source);
            propertyInfo.SetValue(target, value);
        }
    }

    private static object InternalCopy(object originalObject, IDictionary<object, object> visited)
    {
        if (originalObject == null)
        {
            return null;
        }

        var typeToReflect = originalObject.GetType();
        if (IsPrimitive(typeToReflect))
        {
            return originalObject;
        }

        if (visited.TryGetValue(originalObject, out var internalCopy))
        {
            return internalCopy;
        }

        if (typeof(Delegate).IsAssignableFrom(typeToReflect))
        {
            return null;
        }

        var cloneObject = CloneMethod.Invoke(originalObject, null);
        if (typeToReflect.IsArray)
        {
            var arrayType = typeToReflect.GetElementType();
            if (IsPrimitive(arrayType) == false)
            {
                var clonedArray = (Array)cloneObject;
                clonedArray.ForEach((array, indices) => array.SetValue(InternalCopy(clonedArray!.GetValue(indices), visited), indices));
            }
        }

        visited.Add(originalObject, cloneObject);
        CopyFields(originalObject, visited, cloneObject, typeToReflect);
        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
        return cloneObject;
    }

    private static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
    {
        if (typeToReflect.BaseType == null)
        {
            return;
        }

        RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
        CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
    }

    private static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, IReflect typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
    {
        foreach (var fieldInfo in typeToReflect.GetFields(bindingFlags))
        {
            if (filter != null && filter(fieldInfo) == false)
            {
                continue;
            }

            if (IsPrimitive(fieldInfo.FieldType))
            {
                continue;
            }

            var originalFieldValue = fieldInfo.GetValue(originalObject);
            var clonedFieldValue = InternalCopy(originalFieldValue, visited);
            fieldInfo.SetValue(cloneObject, clonedFieldValue);
        }
    }

    private static bool IsPrimitive(this Type type)
    {
        if (type == typeof(string))
        {
            return true;
        }

        return type.IsValueType & type.IsPrimitive;
    }
}

public class ReferenceEqualityComparer : EqualityComparer<object>
{
    public override bool Equals(object x, object y)
    {
        return ReferenceEquals(x, y);
    }

    public override int GetHashCode(object obj)
    {
        return obj.GetHashCode();
    }
}

public static class ArrayExtensions
{
    public static void ForEach(this Array array, Action<Array, int[]> action)
    {
        if (array.LongLength == 0)
        {
            return;
        }

        var walker = new ArrayTraverse(array);
        do
        {
            action(array, walker.Position);
        }
        while (walker.Step());
    }
}

internal class ArrayTraverse
{
    public int[] Position { get; }

    private readonly int[] _maxLengths;

    public ArrayTraverse(Array array)
    {
        _maxLengths = new int[array.Rank];
        for (var i = 0; i < array.Rank; ++i)
        {
            _maxLengths[i] = array.GetLength(i) - 1;
        }

        Position = new int[array.Rank];
    }

    public bool Step()
    {
        for (var i = 0; i < Position.Length; ++i)
        {
            if (Position[i] >= _maxLengths[i])
            {
                continue;
            }

            Position[i]++;
            for (var j = 0; j < i; j++)
            {
                Position[j] = 0;
            }

            return true;
        }

        return false;
    }
}