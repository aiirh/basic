using Aiirh.Audit;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.CommonLibraries.Tests.Audit
{
    [TestFixture]
    public class AuditLogHelperTests
    {
        private static readonly string[] Names = { "John Smith", "Mary Johnson", "David Brown", "Susan Taylor", "James Clark", "Elizabeth Harris", "Robert Lewis", "Karen Young", "George Hall", "Jennifer Walker" };

        [Test]
        public void ToAuditLogs_Empty_ShouldReturnEmpty()
        {
            var result = Enumerable.Empty<Revision>().ToAuditLogs().ToList();
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void ToAuditLogs_OnlyOneRevision_ShouldReturnEmpty(IEnumerable<Revision> revisions, int _)
        {
            var result = revisions.First().MakeCollection().ToAuditLogs().ToList();
            Assert.IsEmpty(result);
        }

        [Test]
        [TestCaseSource(nameof(GetAllSameTestData))]
        public void ToAuditLogs_AllSame_ShouldReturnEmpty(IEnumerable<Revision> revisions)
        {
            var result = revisions.ToAuditLogs().ToList();
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void ToAuditLogs_ValidRevisions_ShouldCorrectValue(IEnumerable<Revision> revisions, int countOfAuditLogs)
        {
            var result = revisions.ToAuditLogs().ToList();
            Assert.AreEqual(countOfAuditLogs, result.Count);
        }

        [Test]
        [TestCaseSource(nameof(GetTestDataDifferentTypes))]
        public void ToAuditLogs_DifferentRevisionTypes_ShouldThrowException(IEnumerable<Revision> revisions)
        {
            Assert.Throws<SimpleException>(() =>
            {
                var _ = revisions.ToAuditLogs().ToList();
            });
        }

        private static IEnumerable<TestCaseData> GetAllSameTestData()
        {
            const string json1 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json2 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json3 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json4 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json5 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            var jsonArray = new[] { json1, json2, json3, json4, json5 };

            var date = DateTime.Now.AddDays(-10);
            var revisions = new List<Revision>();
            foreach (var (json, index) in jsonArray.WithIndex())
            {
                revisions.Add(new Revision(Names[index], json, date.AddDays(index)));
            }

            yield return new TestCaseData(revisions.Shuffle());
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            const string json1 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json2 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test5\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json3 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\", \"XXX\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json4 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json5 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"CHANGE\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            var jsonArray = new[] { json1, json2, json3, json4, json5 };

            var date = DateTime.Now.AddDays(-10);
            var revisions = new List<Revision>();
            foreach (var (json, index) in jsonArray.WithIndex())
            {
                revisions.Add(new Revision(Names[index], json, date.AddDays(index)));
            }

            yield return new TestCaseData(revisions.Shuffle(), 4);
        }

        private static IEnumerable<TestCaseData> GetTestDataDifferentTypes()
        {
            const string json1 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent1\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json2 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent2\",\"Name\":\"Test5\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json3 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent1\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\", \"XXX\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json4 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent2\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json5 = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent1\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"CHANGE\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            var jsonArray = new[] { json1, json2, json3, json4, json5 };

            var date = DateTime.Now.AddDays(-10);
            var revisions = new List<Revision>();
            foreach (var (json, index) in jsonArray.WithIndex())
            {
                revisions.Add(new Revision(Names[index], json, date.AddDays(index)));
            }

            yield return new TestCaseData(revisions.Shuffle());
        }
    }
}
