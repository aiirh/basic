using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Exceptions;

namespace Aiirh.Basic.Utilities
{
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

        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey fromKey, TKey toKey)
        {
            var value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, (TKey, TValue) pair)
        {
            var (key, value) = pair;
            dict.AddOrUpdate(new KeyValuePair<TKey, TValue>(key, value));
        }

        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, KeyValuePair<TKey, TValue> pair)
        {
            if (dict.ContainsKey(pair.Key))
            {
                dict[pair.Key] = pair.Value;
            }
            else
            {
                dict.Add(pair.Key, pair.Value);
            }
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

        public static List<KeyValuePair<string, string>> GetWeekdaysKeyValuePairs()
        {
            return new List<KeyValuePair<string, string>>
                             {
                                 new KeyValuePair<string, string>("Mo", "Monday"),
                                 new KeyValuePair<string, string>("Tu", "Tuesday"),
                                 new KeyValuePair<string, string>("We", "Wednesday"),
                                 new KeyValuePair<string, string>("Th", "Thursday"),
                                 new KeyValuePair<string, string>("Fr", "Friday")
                             };
        }

        public static Dictionary<string, string> GetWeekdaysDict()
        {
            return GetWeekdaysKeyValuePairs().ToDictionary(k => k.Key, k => k.Value);
        }

        public static T3 DictLevel2Get<T1, T2, T3>(this Dictionary<T1, Dictionary<T2, T3>> data, T1 dcCode, T2 stockCode)
        {
            var level3 = default(T3);
            if (data.TryGetValue(dcCode, out var byStock))
            {
                byStock.TryGetValue(stockCode, out level3);
            }
            return level3;
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
                if (!results.ContainsKey(keyValuePair.Key))
                {
                    results.Add(keyValuePair.Key, keyValuePair.Value);
                }
                else
                {
                    results[keyValuePair.Key] = keyValuePair.Value;
                }
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
}