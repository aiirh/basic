using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Aiirh.Audit
{
    public class Revision
    {
        private readonly string _revisionType;

        public Revision(string author, string dataJson, DateTime createdDate, string comment)
        {
            var revisionType = dataJson.GetJsonPropertyByName("RevisionType").ToString(Formatting.None);
            if (string.IsNullOrWhiteSpace(revisionType))
            {
                throw new SimpleException("Revision type is not defined in data JSON");
            }

            var propertyNamesMapping = dataJson.GetJsonPropertyByName("PropertyNamesMapping").ToString(Formatting.None).Convert<Dictionary<string, string>>();
            if (propertyNamesMapping == null)
            {
                throw new SimpleException("PropertyNamesMapping is not defined in data JSON");
            }

            _revisionType = revisionType;
            Author = author;
            DataJson = dataJson;
            CreatedDate = createdDate;
            Comment = comment;
        }

        public string Author { get; }

        public string DataJson { get; }

        public DateTime CreatedDate { get; }

        public string Comment { get; }

        public string GetRevisionType() => _revisionType;

        public bool EqualsAsDataJson(Revision other) => DataJson.EqualsAsJson(other.DataJson);
    }
}
