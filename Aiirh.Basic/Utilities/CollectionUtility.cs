using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Utilities
{
    public static class CollectionUtility
    {
        public static IEnumerable<T> MakeCollection<T>(this T value)
        {
            if (!value.IsNullOrDefault())
            {
                yield return value;
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool AllHaveTheSameValue<T, TPropertyType>(this IEnumerable<T> entities, Func<T, TPropertyType> selector)
        {
            return entities.AllHaveTheSameValue(selector, out _);
        }

        public static bool AllHaveTheSameValue<T, TPropertyType>(this IEnumerable<T> entities, Func<T, TPropertyType> selector, out IList<TPropertyType> differentValues)
        {
            if (entities == null)
            {
                differentValues = Enumerable.Empty<TPropertyType>().ToList();
                return true;
            }
            differentValues = entities.Select(selector).Distinct().ToList();
            return differentValues.Count == 1;
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int nSize = 30)
        {
            for (var i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static IEnumerable<IEnumerable<T>> ToEnumerableBatch<T>(this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                {
                    bucket = new T[size];
                }

                bucket[count++] = item;

                if (count != size)
                {
                    continue;
                }

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                yield return bucket.Take(count);
            }
        }

        public static bool CompareCollections<T>(this IEnumerable<T> first, IEnumerable<T> second) where T : IComparable<T>
        {
            if (first == null || second == null)
            {
                return false;
            }

            var firstList = first.OrderBy(x => x).ToList();
            var secondList = second.OrderBy(x => x).ToList();

            if (firstList.Count != secondList.Count)
            {
                return false;
            }

            for (var i = 0; i < firstList.Count(); i++)
            {
                if (!firstList[i].Equals(secondList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<T> TakeAndRemove<T>(this IList<T> list, int count)
        {
            count = Math.Min(list.Count, count);
            for (var i = 0; i < count; i++)
            {
                var element = list.First();
                list.Remove(element);
                yield return element;
            }
        }

        public static T TakeAndRemove<T>(this IList<T> list)
        {
            var element = list.First();
            list.Remove(element);
            return element;
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).FirstOrDefault();
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).LastOrDefault();
        }

        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return source.Intersect(other).Any();
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> source)
        {
            return source.Select((item, index) => (item, index));
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var rng = new Random();
            var sourceList = source.ToList();
            int count = sourceList.Count;

            for (int i = 0; i < count; i++)
            {
                int randomIndex = rng.Next(0, sourceList.Count);
                yield return sourceList[randomIndex];
                sourceList.RemoveAt(randomIndex);
            }
        }
    }
}
