using System;

namespace Aiirh.Audit
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AuditableAttribute : Attribute
    {
        public AuditableAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}
