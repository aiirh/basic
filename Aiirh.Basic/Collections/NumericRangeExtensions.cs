using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Collections
{
    public static class NumericRangeExtensions
    {
        public static IEnumerable<NumericRange> Exclude(this NumericRange initialRange, IEnumerable<NumericRange> rangesToExclude)
        {
            var reGroupedRanges = rangesToExclude.ReGroupRanges().ToList();
            var newRanges = new List<NumericRange>();

            var currentRange = new NumericRange(initialRange.Begin, initialRange.End);
            foreach (var rangeToExclude in reGroupedRanges)
            {
                var splitRanges = currentRange.Exclude(rangeToExclude).ToList();
                if (splitRanges.Count == 1 && splitRanges.First().Equals(currentRange))
                {
                    continue;
                }

                newRanges.AddRange(splitRanges.Where(x => x.End < initialRange.End));
                currentRange = rangeToExclude.End + 1 <= initialRange.End ? new NumericRange(rangeToExclude.End + 1, initialRange.End) : null;
            }

            if (currentRange != null)
            {
                newRanges.Add(currentRange);
            }

            return newRanges;
        }

        public static IEnumerable<NumericRange> Exclude(this NumericRange initialRange, NumericRange another)
        {
            if (initialRange == null)
            {
                yield break;
            }

            // If no intersection - nothing to exclude (Return initial range)
            if (!initialRange.Intersects(another, out _, out _))
            {
                yield return initialRange;
                yield break;
            }

            // If another covers entire initial - zero range to return (Return 0 ranges)
            if (another.Begin <= initialRange.Begin && another.End >= initialRange.End)
            {
                yield break;
            }

            // If another is completely inside initial - split range (Return 2 ranges)
            var isCompletelyEmbedded = initialRange.Begin < another.Begin && initialRange.End > another.End;
            if (isCompletelyEmbedded)
            {
                yield return new NumericRange(initialRange.Begin, another.Begin - 1);
                yield return new NumericRange(another.End + 1, initialRange.End);
                yield break;
            }

            // If another cuts left part of initial - return right part (Return 1 range)
            var isLeftSideIntersect = another.Begin <= initialRange.Begin && initialRange.End > another.End;
            if (isLeftSideIntersect)
            {
                yield return new NumericRange(another.End + 1, initialRange.End);
                yield break;
            }

            // If another cuts right part of initial - return left part (Return 1 range)
            var isRightSideIntersect = another.End >= initialRange.End && initialRange.Begin < another.Begin;
            if (isRightSideIntersect)
            {
                yield return new NumericRange(initialRange.Begin, another.Begin - 1);
                yield break;
            }
        }

        public static IEnumerable<NumericRange> Concat(this IEnumerable<NumericRange> ranges, NumericRange range)
        {
            var rangesList = ranges.ToList();
            rangesList.Add(range);
            return rangesList.ReGroupRanges();
        }

        public static IEnumerable<NumericRange> Concat(this IEnumerable<NumericRange> ranges, IEnumerable<int> values)
        {
            var newRanges = ranges.ReGroupRanges().ToList();
            foreach (var value in values)
            {
                if (newRanges.Any(x => x.Contains(value)))
                {
                    continue;
                }

                var possibleToAdd = newRanges.FirstOrDefault(x => x.CanBeAddedToHeadOrTail(value));
                if (possibleToAdd == null)
                {
                    newRanges.Add(new NumericRange(value));
                    newRanges = newRanges.ReGroupRanges().ToList();
                    continue;
                }

                possibleToAdd.Add(value);
                newRanges = newRanges.ReGroupRanges().ToList();
            }

            return newRanges;
        }

        public static IEnumerable<long> TakeValues(this IEnumerable<NumericRange> rangesToExclude, int count)
        {
            var reGroupedValueToExclude = rangesToExclude.ReGroupRanges().ToList();
            var possibleCount = reGroupedValueToExclude.TotalCountUnsafe();
            if (possibleCount < count)
            {
                throw new ArgumentException($"There is not enough values to take. Available {possibleCount}, but requested {count}]");
            }

            for (var i = 0; i < count; i++)
            {
                var first = reGroupedValueToExclude.First();
                yield return first.GetFirstAndRemove();
                if (first.Count == 0)
                {
                    reGroupedValueToExclude.RemoveAt(0);
                }
            }
        }

        public static long TotalCount(this IEnumerable<NumericRange> ranges)
        {
            return ranges.ReGroupRanges().Sum(x => x.Count);
        }

        internal static IEnumerable<NumericRange> ReGroupRanges(this IEnumerable<NumericRange> ranges)
        {
            var rangesList = ranges.ToList();

            if (rangesList.Count < 1)
            {
                return rangesList;
            }

            var sorted = rangesList.OrderBy(x => x).ToList();
            var result = new List<NumericRange>();
            var current = sorted[0];
            for (var i = 1; i < sorted.Count; i++)
            {
                var next = sorted[i];
                if (current.IntersectsOrCommonBorder(next, out var first, out var second))
                {
                    current = first.MergeUnsafe(second);
                }
                else
                {
                    result.Add(current);
                    current = next;
                }
            }

            result.Add(current);
            return result;
        }

        public static bool Intersects(this NumericRange current, NumericRange another, out NumericRange first, out NumericRange second, bool borderIncluded = true)
        {
            var isCurrentGreater = current.CompareTo(another) > 0;
            first = isCurrentGreater ? another : current;
            second = isCurrentGreater ? current : another;
            var isEmbedded = first.Begin <= second.Begin && first.End >= second.End;
            var borderShift = borderIncluded ? 1 : 0;
            return isEmbedded || second.Begin < first.End + borderShift;
        }

        public static bool Intersects(this IEnumerable<NumericRange> ranges, NumericRange range, bool borderIncluded = true)
        {
            return ranges.Any(x => x.Intersects(range, out _, out _, borderIncluded));
        }

        public static bool IntersectsOrCommonBorder(this NumericRange current, NumericRange another, out NumericRange first, out NumericRange second)
        {
            var intersects = current.Intersects(another, out first, out second);
            return intersects || second.Begin <= first.End + 1;
        }

        public static NumericRange Merge(this NumericRange current, NumericRange another)
        {
            if (!current.IntersectsOrCommonBorder(another, out var first, out var second))
            {
                throw new ArgumentException($"Range [{first.Begin}, {first.End}] doesn't intersect with range [{second.Begin}, {second.End}]");
            }

            return first.MergeUnsafe(second);
        }

        private static long TotalCountUnsafe(this IEnumerable<NumericRange> ranges)
        {
            return ranges.Sum(x => x.Count);
        }

        private static NumericRange MergeUnsafe(this NumericRange first, NumericRange second)
        {
            var min = Math.Min(first.Begin, second.Begin);
            var max = Math.Max(first.End, second.End);
            return new NumericRange(min, max);
        }
    }
}
