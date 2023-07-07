using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Collections
{
    public static class GenericRangeExtensions
    {
        public static IEnumerable<Range<T>> Concat<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            var numericRanges = ranges.Select(x => x.GetNumericRange());
            var numericRange = range.GetNumericRange();
            var mergedRanges = numericRanges.Concat(numericRange);
            return mergedRanges.Select(x => new Range<T>(x.Begin.GetValueFromLong<T>(), x.End.GetValueFromLong<T>()));
        }

        public static bool Intersects<T>(this Range<T> one, Range<T> another)
        {
            return one.GetNumericRange().Intersects(another.GetNumericRange(), out _, out _);
        }

        public static bool Intersects<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.IntersectionsCount(range) > 0;
        }

        public static int IntersectionsCount<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.Count(x => x.GetNumericRange().Intersects(range.GetNumericRange(), out _, out _));
        }

        public static bool IsEmbedded<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.Select(x => x.GetNumericRange()).IsEmbedded(range.GetNumericRange());
        }

        public static bool IntersectsBorderNotIncluded<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.IntersectionsBorderNotIncludedCount(range) > 0;
        }

        public static int IntersectionsBorderNotIncludedCount<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.GetIntersectionsBorderNotIncluded(range).Count();
        }

        public static IEnumerable<Range<T>> GetIntersectionsBorderNotIncluded<T>(this IEnumerable<Range<T>> ranges, Range<T> range)
        {
            return ranges.Where(x => x.GetNumericRange().Intersects(range.GetNumericRange(), out _, out _, false));
        }

        public static IEnumerable<Range<T>> Inverse<T>(this IEnumerable<Range<T>> ranges)
        {
            var orderedRange = ranges.OrderBy(x => x.Begin).ToList();
            if (orderedRange.Count < 2)
            {
                // Can't inverse range if it has less than 2 elements
                return Enumerable.Empty<Range<T>>();
            }

            IEnumerable<Range<T>> result = new List<Range<T>>();
            for (var i = 1; i < orderedRange.Count; i++)
            {
                var previousTaken = orderedRange[i - 1];
                var nextTaken = orderedRange[i];
                var rangeBeforeNextTaken = new Range<T>(previousTaken.End, nextTaken.Begin);
                result = result.Concat(rangeBeforeNextTaken);
            }

            return result;
        }
    }
}
