using System;
using System.Collections.Generic;
using System.Linq;
using Range = Aiirh.Collections.Models.Range;

namespace Aiirh.Collections.Extensions
{
    public static class NumberRangeExtensions
    {

        public static IEnumerable<Range> Exclude(this Range initialRange, IEnumerable<Range> rangesToExclude)
        {
            var reGroupedRanges = rangesToExclude.ReGroupRanges().ToList();
            var newRanges = new List<Range>();

            var currentRange = new Range(initialRange.Begin, initialRange.End);
            foreach (var rangeToExclude in reGroupedRanges)
            {
                var splitRanges = currentRange.Exclude(rangeToExclude).ToList();
                if (splitRanges.Count == 1 && splitRanges.First().Equals(currentRange))
                {
                    continue;
                }

                newRanges.AddRange(splitRanges.Where(x => x.End < initialRange.End));
                currentRange = rangeToExclude.End + 1 <= initialRange.End ? new Range(rangeToExclude.End + 1, initialRange.End) : null;
            }

            if (currentRange != null)
            {
                newRanges.Add(currentRange);
            }
            return newRanges;
        }

        public static IEnumerable<Range> Exclude(this Range initialRange, Range another)
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
                yield return new Range(initialRange.Begin, another.Begin - 1);
                yield return new Range(another.End + 1, initialRange.End);
                yield break;
            }

            // If another cuts left part of initial - return right part (Return 1 range)
            var isLeftSideIntersect = another.Begin <= initialRange.Begin && initialRange.End > another.End;
            if (isLeftSideIntersect)
            {
                yield return new Range(another.End + 1, initialRange.End);
                yield break;
            }

            // If another cuts right part of initial - return left part (Return 1 range)
            var isRightSideIntersect = another.End >= initialRange.End && initialRange.Begin < another.Begin;
            if (isRightSideIntersect)
            {
                yield return new Range(initialRange.Begin, another.Begin - 1);
                yield break;
            }
        }

        public static IEnumerable<Range> Concat(this IEnumerable<Range> ranges, Range range)
        {
            var rangesList = ranges.ToList();
            rangesList.Add(range);
            return rangesList.ReGroupRanges();
        }

        public static IEnumerable<Range> Concat(this IEnumerable<Range> ranges, IEnumerable<int> values)
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
                    newRanges.Add(new Range(value));
                    newRanges = newRanges.ReGroupRanges().ToList();
                    continue;
                }

                possibleToAdd.Add(value);
                newRanges = newRanges.ReGroupRanges().ToList();
            }
            return newRanges;
        }

        public static IEnumerable<int> TakeValues(this IEnumerable<Range> rangesToExclude, int count)
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

        public static int TotalCount(this IEnumerable<Range> ranges)
        {
            return ranges.ReGroupRanges().Sum(x => x.Count);
        }

        internal static IEnumerable<Range> ReGroupRanges(this IEnumerable<Range> ranges)
        {
            var rangesList = ranges.ToList();

            if (rangesList.Count < 1)
            {
                return rangesList;
            }

            var sorted = rangesList.OrderBy(x => x).ToList();
            var result = new List<Range>();
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

        public static bool Intersects(this Range current, Range another, out Range first, out Range second)
        {
            var isCurrentGreater = current.CompareTo(another) > 0;
            first = isCurrentGreater ? another : current;
            second = isCurrentGreater ? current : another;
            var isEmbedded = first.Begin <= second.Begin && first.End >= second.End;
            return isEmbedded || second.Begin < first.End + 1;
        }

        public static bool IntersectsOrCommonBorder(this Range current, Range another, out Range first, out Range second)
        {
            var intersects = current.Intersects(another, out first, out second);
            return intersects || second.Begin <= first.End + 1;
        }

        public static Range Merge(this Range current, Range another)
        {
            if (!current.IntersectsOrCommonBorder(another, out var first, out var second))
            {
                throw new ArgumentException($"Range [{first.Begin}, {first.End}] doesn't intersect with range [{second.Begin}, {second.End}]");
            }
            return first.MergeUnsafe(second);
        }

        private static int TotalCountUnsafe(this IEnumerable<Range> ranges)
        {
            return ranges.Sum(x => x.Count);
        }

        private static Range MergeUnsafe(this Range first, Range second)
        {
            var min = Math.Min(first.Begin, second.Begin);
            var max = Math.Max(first.End, second.End);
            return new Range(min, max);
        }
    }
}
