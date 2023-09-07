using System;

namespace Aiirh.Basic.Audit.Contract
{
    public interface IRevision
    {
        string Author { get; }

        string DataJson { get; }

        DateTime CreatedDate { get; }
    }
}
