using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Validation;

public static class ValidationResultExtensions
{
    /// <summary>
    /// Combines messages that belong to the same InvalidEntity using the default equality comparer.
    /// The resulting messages are distinct (duplicates are removed).
    /// </summary>
    /// <typeparam name="T">The type of the InvalidEntity.</typeparam>
    /// <param name="validationResults">The collection of validation results to combine.</param>
    /// <returns>
    /// An IEnumerable of ValidationResult&lt;T&gt; where messages for each InvalidEntity are combined and distinct.
    /// </returns>
    public static IEnumerable<ValidationResult<T>> CombineByEntity<T>(this IEnumerable<ValidationResult<T>> validationResults)
    {
        return validationResults.GroupBy(x => x.InvalidEntity).Select(g => new ValidationResult<T>
        {
            InvalidEntity = g.Key,
            Messages = g.SelectMany(x => x.Messages).DistinctBy(x => x.Message.ToString())
        });
    }

#if NETSTANDARD
    private static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
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
#endif
}