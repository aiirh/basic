using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Extensions
{
    public static class CollectionExtensions
    {
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

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).FirstOrDefault();
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).LastOrDefault();
        }
    }
}
