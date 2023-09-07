using Aiirh.Basic.Audit.Contract;
using System;
using System.Collections.Generic;

namespace Aiirh.Basic.Audit
{
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

        public string Author { get; }

        public DateTime CreatedDate { get; }

        public AuditLog(DateTime createdDate, string author)
        {
            CreatedDate = createdDate;
            Author = author;
        }

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
