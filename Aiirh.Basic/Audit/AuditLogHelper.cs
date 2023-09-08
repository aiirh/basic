using Aiirh.Basic.Audit.Contract;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Audit
{
    public static class AuditLogHelper
    {
        public static Revision CreateRevision<T>(this T data, DateTime createdDate, string author)
        {
            if (data is null)
            {
                return null;
            }

            var json = data.ToRevisionJson();
            return new Revision(author, json, createdDate);
        }

        public static IEnumerable<IAuditLog> ToAuditLogs(this IEnumerable<Revision> revisions)
        {
            var sortedRevisions = revisions.OrderBy(x => x.CreatedDate).ToArray();
            if (sortedRevisions.Length < 2)
            {
                yield break;
            }

            sortedRevisions.ValidateRevisions();

            for (var i = 0; i < sortedRevisions.Length - 1; i++)
            {
                var earlier = sortedRevisions[i];
                var later = sortedRevisions[i + 1];
                if (later.EqualsAsDataJson(earlier))
                {
                    continue;
                }

                var auditLog = AuditLogBuilder.Build(earlier.DataJson, later.DataJson, later.CreatedDate, later.Author);
                if (auditLog.Changes.Any())
                {
                    yield return auditLog;
                }
            }
        }

        private static void ValidateRevisions(this IEnumerable<Revision> revisions)
        {
            if (!revisions.AllHaveTheSameValue(x => x.GetRevisionType()))
            {
                throw new SimpleException("Revisions must have same revision type");
            }
        }
    }
}
