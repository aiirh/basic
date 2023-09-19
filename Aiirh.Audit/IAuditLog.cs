using System;
using System.Collections.Generic;

namespace Aiirh.Audit
{
    public interface IAuditLogEntry
    {
        string FullPath { get; }

        string PropertyName { get; }

        public string[] PathSegments { get; }

        string OldValue { get; }

        string NewValue { get; }

        ChangeType ChangeType { get; }
    }

    public interface IAuditLog
    {
        string Author { get; }

        DateTime CreatedDate { get; }

        IEnumerable<IAuditLogEntry> Changes { get; }
    }

    public enum ChangeType
    {
        Edit,
        Add,
        Remove
    }
}
