using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Audit.Contract
{
    public interface IAuditLogEntry
    {
        string PropertyName { get; }

        string OldValue { get; }

        string NewValue { get; }

        ChangeType ChangeType { get; }
    }

    public interface IAuditLog
    {
        string Author { get; }

        DateTime CreatedDate { get; }

        IEnumerable<IAuditLogEntry> Entries { get; }
    }

    public enum ChangeType
    {
        Edit,
        Add,
        Remove
    }
}
