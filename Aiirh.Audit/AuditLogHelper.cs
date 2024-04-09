using Aiirh.Audit.Internal;
using Aiirh.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Audit;

public static class AuditLogHelper
{
    public static Revision CreateRevision<T>(this T data, DateTime createdDate, string author, string comment)
    {
        if (data is null)
        {
            return null;
        }

        var json = data.ToRevisionJson();
        return string.IsNullOrWhiteSpace(json) ? null : new Revision(author, json, createdDate, comment);
    }

    public static IEnumerable<IAuditLog> ToAuditLogs(this IEnumerable<Revision> revisions, int? depth = null, string pathSeparator = "->")
    {
        var totalNumberOfChanges = 0;
        var isLimited = depth is > 0;
        var revisionsAsArray = revisions.ToArray();
        if (revisionsAsArray.Length < 2)
        {
            yield break;
        }

        var sortedRevisions = revisionsAsArray.ValidateRevisions(pathSeparator).TakeLastRevisionType().OrderByDescending(x => x.CreatedDate).ToArray();
        if (sortedRevisions.Length < 2)
        {
            yield break;
        }

        for (var i = 0; i < sortedRevisions.Length - 1; i++)
        {
            var later = sortedRevisions[i];
            var earlier = sortedRevisions[i + 1];
            if (later.EqualsAsDataJson(earlier))
            {
                continue;
            }

            var auditLog = AuditLogBuilder.Build(earlier.DataJson, later.DataJson, later.CreatedDate, later.Author, later.Comment, pathSeparator);
            if (!auditLog.Changes.Any())
            {
                continue;
            }

            yield return auditLog;
            totalNumberOfChanges += auditLog.Changes.Count();
            if (isLimited && totalNumberOfChanges >= depth.Value)
            {
                yield break;
            }
        }
    }

    private static ICollection<Revision> ValidateRevisions(this ICollection<Revision> revisions, string pathSeparator)
    {
        if (revisions.Select(x => x.DataJson).Any(x => x.Contains(pathSeparator)))
        {
            throw new SimpleException("This separator can't be used because it participates in some property names. Choose another separator");
        }

        return revisions;
    }

    private static IEnumerable<Revision> TakeLastRevisionType(this ICollection<Revision> revisions)
    {
        var latestRevisionType = revisions.MaxBy(x => x.CreatedDate).GetRevisionType();
        return revisions.Where(x => x.GetRevisionType().Equals(latestRevisionType, StringComparison.InvariantCultureIgnoreCase));
    }
}
