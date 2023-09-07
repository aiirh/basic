using Aiirh.Basic.Audit.Contract;
using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
    public static class AuditLogHelper
    {
        public static IRevision CreateRevision<T>(this T data, DateTime createDate, string author)
        {
            var json = data.ToRevisionJson();
            return new Revision(author, json, createDate);
        }

        public static IEnumerable<IAuditLog> ToAuditLogs(this IEnumerable<IRevision> revisions)
        {
            return null;
        }
    }
}
