using Aiirh.Basic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Audit.Internal
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

        public string PropertyName { get; internal set; }

        public string OldValue { get; }

        public string NewValue { get; }

        public ChangeType ChangeType { get; }
    }

    internal class AuditLog : IAuditLog
    {
        private readonly List<AuditLogEntry> _entries = new List<AuditLogEntry>();

        public IEnumerable<IAuditLogEntry> Changes => _entries;

        public string Author { get; }

        public DateTime CreatedDate { get; }

        public AuditLog(DateTime createdDate, string author)
        {
            CreatedDate = createdDate;
            Author = author;
        }

        public void AddEntries(IEnumerable<AuditLogEntry> entries, IDictionary<string, string> propertyNamesMapping)
        {
            var auditLogEntries = entries.ToList();
            auditLogEntries.ForEach(x =>
            {
                foreach (KeyValuePair<string, string> pair in propertyNamesMapping)
                {
                    if (x.PropertyName.Contains(pair.Key))
                    {
                        x.PropertyName = x.PropertyName.Replace(pair.Key, pair.Value);
                    }
                }
            });

            _entries.AddRange(auditLogEntries);
        }

        public void AddEntriesFromAnotherAuditLog(AuditLog auditLog, IDictionary<string, string> propertyNamesMapping)
        {
            AddEntries(auditLog._entries, propertyNamesMapping);
        }

        public void AddEntry(AuditLogEntry entry, IDictionary<string, string> propertyNamesMapping)
        {
            AddEntries(entry.MakeCollection(), propertyNamesMapping);
        }
    }
}
