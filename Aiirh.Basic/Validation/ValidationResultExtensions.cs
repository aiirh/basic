using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Validation;

public static class ValidationResultExtensions
{
    /// <summary>
    /// Combine messages that belong to the same InvalidEntity using default equality comparer. Result messages are distinct (duplicates are removed)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="validationResults"></param>
    /// <returns></returns>
    public static IEnumerable<ValidationResult<T>> CombineByEntity<T>(this IEnumerable<ValidationResult<T>> validationResults)
    {
        return validationResults.GroupBy(x => x.InvalidEntity).Select(g => new ValidationResult<T>
        {
            InvalidEntity = g.Key,
            Messages = g.SelectMany(x => x.Messages).DistinctBy(x => x.Message.ToString())
        });
    }

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
}