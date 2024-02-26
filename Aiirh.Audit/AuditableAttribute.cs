using System;

namespace Aiirh.Audit;

[AttributeUsage(AttributeTargets.Property)]
public class AuditableAttribute : Attribute
{
    public string PropertyName { get; set; }

    public string DisplayName { get; set; }
}