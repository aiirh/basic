using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Exceptions;

namespace Aiirh.Basic.Utilities;

public static class DictionaryUtility
{
    public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
    {
        if (key == null || dict == null)
        {
            return defaultValue;
        }

        if (!dict.TryGetValue(key, out var ret))
        {
            ret = defaultValue;
        }

        return ret;
    }

    /// <summary>
    /// Gets the value from the dictionary for the specified key.
    /// If the key is not found, inserts the provided default value into the dictionary for the provided key and returns it.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dict">The dictionary to retrieve the value from or insert the default value into.</param>
    /// <param name="key">The key to locate in the dictionary.</param>
    /// <param name="defaultValue">The value to insert if the key is not found.</param>
    /// <returns>The value associated with the specified key, or the default value if the key was not found.</returns>
    public static TValue GetValueOrAddDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
    {
        if (dict.TryGetValue(key, out var existing))
        {
            return existing;
        }

        dict.Add(key, defaultValue);
        return defaultValue;
    }

    public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
    {
        var value = dic[fromKey];
        dic.Remove(fromKey);
        dic[toKey] = value;
    }

    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, (TKey Key, TValue Value) pair)
    {
        var (key, value) = pair;
        dict.AddOrUpdate(new KeyValuePair<TKey, TValue>(key, value));
    }

    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
    {
        dict[pair.Key] = pair.Value;
    }

    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        dict.AddOrUpdate(new KeyValuePair<TKey, TValue>(key, value));
    }

    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict.ContainsKey(key))
        {
            return false;
        }

        dict[key] = value;
        return true;
    }

    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
    {
        return dict.TryAdd(pair.Key, pair.Value);
    }

    public static IDictionary<T1, T2> Merge<T1, T2>(this IDictionary<T1, T2> first, IDictionary<T1, T2> second)
    {
        if (first == null)
        {
            return second;
        }

        if (second == null)
        {
            return first;
        }

        var results = new Dictionary<T1, T2>(first);
        foreach (var keyValuePair in second)
        {
            results[keyValuePair.Key] = keyValuePair.Value;
        }

        return results;
    }

    public static IDictionary<T1, T2> Merge<T1, T2>(this IDictionary<T1, T2> first, IDictionary<T1, T2> second, Func<T2, T2, T2> mergeMethod)
    {
        if (first == null)
        {
            return second;
        }

        if (second == null)
        {
            return first;
        }

        var results = new Dictionary<T1, T2>(first);
        foreach (var keyValuePair in second)
        {
            if (!results.ContainsKey(keyValuePair.Key))
            {
                results.Add(keyValuePair.Key, keyValuePair.Value);
            }
            else
            {
                results[keyValuePair.Key] = mergeMethod(results[keyValuePair.Key], keyValuePair.Value);
            }
        }

        return results;
    }

    public static Dictionary<T1, T2> ToDictionary<T1, T2>(this IDictionary<T1, T2> dictionary)
    {
        if (dictionary == null)
        {
            return null;
        }

        return dictionary is Dictionary<T1, T2> casted ? casted : dictionary.ToDictionary(x => x.Key, x => x.Value);
    }

    public static bool TryGetKeyByOneOfValues<TKey, TValue>(this IDictionary<TKey, IEnumerable<TValue>> dict, TValue valueToSearch, out TKey key)
    {
        var result = dict.Where(x => x.Value.Contains(valueToSearch)).ToList();
        if (result.Count == 1)
        {
            key = result[0].Key;
            return true;
        }

        key = default;
        return false;
    }

    public static TValue GetValueOrException<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, string exceptionMessage)
    {
        if (dict == null)
        {
            throw new SimpleException($"Can't get value for key {key}", "Dictionary is null");
        }

        if (dict.TryGetValue(key, out var value))
        {
            return value;
        }

        throw new SimpleException(exceptionMessage);
    }

    public static TValue GetValueOrException<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        return dict.GetValueOrException(key, $"Key {key} was not found in dictionary");
    }
}