using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using Newtonsoft.Json;
using System;

namespace Aiirh.Basic.Audit.Contract
{
    public class Revision
    {
        private readonly string _revisionType;

        public Revision(string author, string dataJson, DateTime createdDate)
        {
            var revisionType = dataJson.GetJsonPropertyByName("RevisionType").ToString(Formatting.None);
            if (string.IsNullOrWhiteSpace(revisionType))
            {
                throw new SimpleException("Revision type is not defined in data JSON");
            }

            _revisionType = revisionType;
            Author = author;
            DataJson = dataJson;
            CreatedDate = createdDate;
        }

        public string Author { get; }

        public string DataJson { get; }

        public DateTime CreatedDate { get; }

        public string GetRevisionType() => _revisionType;

        public bool EqualsAsDataJson(Revision other) => DataJson.EqualsAsJson(other.DataJson);
    }
}
