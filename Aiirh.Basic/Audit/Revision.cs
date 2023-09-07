using Aiirh.Basic.Audit.Contract;
using System;

namespace Aiirh.Basic.Audit
{
    internal class Revision : IRevision
    {
        public Revision(string author, string dataJson, DateTime createdDate)
        {
            Author = author;
            DataJson = dataJson;
            CreatedDate = createdDate;
        }

        public string Author { get; }

        public string DataJson { get; }

        public DateTime CreatedDate { get; }
    }
}
