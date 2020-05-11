using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Validation;

namespace Aiirh.Basic.Extensions
{
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
                Messages = g.SelectMany(x => x.Messages).DistinctBy(x => x.WebMessage.ToString())
            });
        }
    }
}
