using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
    public interface IAuditLogEntry
    {
        string PropertyName { get; }

        string OldValue { get; }

        string NewValue { get; }
    }

    public interface IAuditLog
    {
        IEnumerable<IAuditLogEntry> Entries { get; }
    }

    internal class AuditLogEntry : IAuditLogEntry
    {
        public string PropertyName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
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
