using Aiirh.Audit.Internal;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Audit
{
    public static class AuditLogHelper
    {
        public static Revision CreateRevision<T>(this T data, DateTime createdDate, string author, string comment)
        {
            if (data is null)
            {
                return null;
            }

            var json = data.ToRevisionJson();
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return new Revision(author, json, createdDate, comment);
        }

        public static IEnumerable<IAuditLog> ToAuditLogs(this IEnumerable<Revision> revisions, string pathSeparator = "->")
        {
            var sortedRevisions = revisions.OrderBy(x => x.CreatedDate).ToArray();
            if (sortedRevisions.Length < 2)
            {
                yield break;
            }

            sortedRevisions.ValidateRevisions(pathSeparator);

            for (var i = 0; i < sortedRevisions.Length - 1; i++)
            {
                var earlier = sortedRevisions[i];
                var later = sortedRevisions[i + 1];
                if (later.EqualsAsDataJson(earlier))
                {
                    continue;
                }

                var auditLog = AuditLogBuilder.Build(earlier.DataJson, later.DataJson, later.CreatedDate, later.Author, later.Comment, pathSeparator);
                if (auditLog.Changes.Any())
                {
                    yield return auditLog;
                }
            }
        }

        private static void ValidateRevisions(this ICollection<Revision> revisions, string pathSeparator)
        {
            if (!revisions.AllHaveTheSameValue(x => x.GetRevisionType()))
            {
                throw new SimpleException("Revisions must have same revision type");
            }

            if (revisions.Select(x => x.DataJson).Any(x => x.Contains(pathSeparator)))
            {
                throw new SimpleException("This separator can't be used because it participates in some property names. Choose another separator");
            }
        }
    }
}
