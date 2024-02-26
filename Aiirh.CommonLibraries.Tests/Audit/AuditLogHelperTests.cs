using Aiirh.Audit;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.CommonLibraries.Tests.Audit;

[TestFixture]
public class AuditLogHelperTests
{
    private static readonly string[] Names = { "John Smith", "Mary Johnson", "David Brown", "Susan Taylor", "James Clark", "Elizabeth Harris", "Robert Lewis", "Karen Young", "George Hall", "Jennifer Walker" };

    [Test]
    public void ToAuditLogs_Empty_ShouldReturnEmpty()
    {
        var result = Enumerable.Empty<Revision>().ToAuditLogs().ToList();
        ClassicAssert.AreEqual(0, result.Count);
    }

    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void ToAuditLogs_OnlyOneRevision_ShouldReturnEmpty(IEnumerable<Revision> revisions, int _)
    {
        var result = revisions.First().MakeCollection().ToAuditLogs().ToList();
        ClassicAssert.IsEmpty(result);
    }

    [Test]
    [TestCaseSource(nameof(GetAllSameTestData))]
    public void ToAuditLogs_AllSame_ShouldReturnEmpty(IEnumerable<Revision> revisions)
    {
        var result = revisions.ToAuditLogs().ToList();
        ClassicAssert.AreEqual(0, result.Count);
    }

    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void ToAuditLogs_ValidRevisions_ShouldCorrectValue(IEnumerable<Revision> revisions, int countOfAuditLogs)
    {
        var result = revisions.ToAuditLogs().ToList();
        ClassicAssert.AreEqual(countOfAuditLogs, result.Count);
    }

    [Test]
    [TestCaseSource(nameof(GetTestDataDifferentTypes))]
    public void ToAuditLogs_DifferentRevisionTypes_ShouldTakeLatestType(IEnumerable<Revision> revisions, int countOfAuditLogs)
    {
        var result = revisions.ToAuditLogs().ToList();
        ClassicAssert.AreEqual(countOfAuditLogs, result.Count);
    }

    [Test]
    [TestCaseSource(nameof(GetTestDataWrongSeparator))]
    public void ToAuditLogs_DifferentWrongSeparator_ShouldThrowException(IEnumerable<Revision> revisions, string separator)
    {
        var exception = Assert.Throws<SimpleException>(() =>
        {
            var _ = revisions.ToAuditLogs(separator).ToList();
        });
        ClassicAssert.AreEqual("This separator can't be used because it participates in some property names. Choose another separator", exception.Message);
    }

    private static IEnumerable<TestCaseData> GetAllSameTestData()
    {
        const string json1 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json2 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json3 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json4 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json5 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        var jsonArray = new[] { json1, json2, json3, json4, json5 };

        var date = DateTime.Now.AddDays(-10);
        var revisions = new List<Revision>();
        foreach (var (json, index) in jsonArray.WithIndex())
        {
            revisions.Add(new Revision(Names[index], json, date.AddDays(index), "Comment"));
        }

        yield return new TestCaseData(revisions.Shuffle());
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        const string json1 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json2 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test5\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json3 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\", \"XXX\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json4 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json5 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"CHANGE\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        var jsonArray = new[] { json1, json2, json3, json4, json5 };

        var date = DateTime.Now.AddDays(-10);
        var revisions = new List<Revision>();
        foreach (var (json, index) in jsonArray.WithIndex())
        {
            revisions.Add(new Revision(Names[index], json, date.AddDays(index), "Comment"));
        }

        yield return new TestCaseData(revisions.Shuffle(), 4);
    }

    private static IEnumerable<TestCaseData> GetTestDataDifferentTypes()
    {
        const string json1 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent1\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json2 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent2\",\"Name\":\"Test5\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json3 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\", \"XXX\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json4 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        const string json5 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"CHANGE\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"ChildName\":\"Child name\",\"ChildDescription\":\"Child description\"}}";
        var jsonArray = new[] { json1, json2, json3, json4, json5 };

        var date = DateTime.Now.AddDays(-10);
        var revisions = new List<Revision>();
        foreach (var (json, index) in jsonArray.WithIndex())
        {
            revisions.Add(new Revision(Names[index], json, date.AddDays(index), "Comment"));
        }

        yield return new TestCaseData(revisions.Shuffle(), 2);
    }

    private static IEnumerable<TestCaseData> GetTestDataWrongSeparator()
    {
        var date = DateTime.Now.AddDays(-10);

        const string json1 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"EmbeddedArray\"}}";
        const string json2 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"Embedded|Array\"}}";
        const string json3 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"EmbeddedArray\"}}";
        var jsonArray123 = new[] { json1, json2, json3 };

        var revisions123 = new List<Revision>();
        foreach (var (json, index) in jsonArray123.WithIndex())
        {
            revisions123.Add(new Revision(Names[index], json, date.AddDays(index), "Comment"));
        }

        const string json4 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"EmbeddedArray\"}}";
        const string json5 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"EmbeddedArray\"}}";
        const string json6 = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"Embedded -> Array\"}}";
        var jsonArray456 = new[] { json4, json5, json6 };

        var revisions456 = new List<Revision>();
        foreach (var (json, index) in jsonArray456.WithIndex())
        {
            revisions456.Add(new Revision(Names[index], json, date.AddDays(index), "Comment"));
        }

        yield return new TestCaseData(revisions123.Shuffle(), "|");
        yield return new TestCaseData(revisions456.Shuffle(), " -> ");
    }
}