using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
    public enum ChangeType
    {
        Edit,
        Add,
        Remove
    }

    public interface IAuditLogEntry
    {
        string PropertyName { get; }

        string OldValue { get; }

        string NewValue { get; }

        ChangeType ChangeType { get; }
    }

    public interface IAuditLog
    {
        IEnumerable<IAuditLogEntry> Entries { get; }
    }

    internal class AuditLogEntry : IAuditLogEntry
    {
        private AuditLogEntry(ChangeType changeType, string propertyName, string newValue, string oldValue)
        {
            ChangeType = changeType;
            PropertyName = propertyName;
            NewValue = newValue;
            OldValue = oldValue;
        }

        public static AuditLogEntry Edit(string propertyName, string newValue, string oldValue)
        {
            return new AuditLogEntry(ChangeType.Edit, propertyName, newValue, oldValue);
        }

        public static AuditLogEntry Remove(string propertyName, string oldValue)
        {
            return new AuditLogEntry(ChangeType.Remove, propertyName, null, oldValue);
        }

        public static AuditLogEntry Add(string propertyName, string newValue)
        {
            return new AuditLogEntry(ChangeType.Add, propertyName, newValue, null);
        }

        public string PropertyName { get; }

        public string OldValue { get; }

        public string NewValue { get; }

        public ChangeType ChangeType { get; }
    }

    internal class AuditLog : IAuditLog
    {
        private readonly List<AuditLogEntry> _entries = new List<AuditLogEntry>();

        public IEnumerable<IAuditLogEntry> Entries => _entries;

        public void AddEntry(AuditLogEntry entry)
        {
            _entries.Add(entry);
        }

        public void AddEntries(IEnumerable<AuditLogEntry> entries)
        {
            _entries.AddRange(entries);
        }

        public void AddEntriesFromAnotherAuditLog(AuditLog auditLog)
        {
            _entries.AddRange(auditLog._entries);
        }
    }
}
