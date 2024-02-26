using Aiirh.Audit;
using Aiirh.Audit.Internal;
using NUnit.Framework;
using System.Collections.Generic;
using NUnit.Framework.Legacy;

namespace Aiirh.CommonLibraries.Tests.Audit;

[TestFixture]
public class RevisionCreatorTests
{
    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void ToRevisionJson_ShouldReturnCorrectValue<T>(T data, string expected)
    {
        var result = data.ToRevisionJson();
        ClassicAssert.AreEqual(expected, result);
    }

    public class MyAuditTestParent
    {
        public string Ignore1 { get; set; }

        [Auditable(PropertyName = "Name", DisplayName = "Some name for Property2")]
        public string Property2 { get; set; }

        [Auditable(DisplayName = "Description")]
        public string Property3 { get; set; }

        [Auditable(PropertyName = "NullToBeHere")]
        public string Null1 { get; set; }

        public string[] ListIgnored { get; set; }

        [Auditable(PropertyName = "StringArray")]
        public string[] ListIncluded { get; set; }

        [Auditable(PropertyName = "ObjectArray")]
        public MyAuditTestChild[] ComplexListIncluded { get; set; }

        [Auditable(PropertyName = "VeryDeepArray")]
        public MyDeepAuditTestChild[] DeepCollection { get; set; }
    }

    public class MyAuditTestChild
    {
        public string Ignore1 { get; set; }

        [Auditable(PropertyName = "ChildName")]
        public string Property2 { get; set; }

        [Auditable(PropertyName = "ChildDescription")]
        public string Property3 { get; set; }
    }

    public class MyDeepAuditTestChild
    {
        public string Ignore1 { get; set; }

        [Auditable(PropertyName = "DeepChildName")]
        public string Property2 { get; set; }

        [Auditable(PropertyName = "DeepChildDescription")]
        public string Property3 { get; set; }

        [Auditable]
        public MyAuditTestChild EmbeddedObject { get; set; }

        [Auditable]
        public MyAuditTestChild[] EmbeddedArray { get; set; }
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        var myAuditTestChildArray1 = new[]
        {
            new MyAuditTestChild { Ignore1 = "C1", Property2 = "C2", Property3 = "C3" },
            new MyAuditTestChild { Ignore1 = "C4", Property2 = "C5", Property3 = "C6" }
        };

        var myAuditTestChildArray2 = new[]
        {
            new MyAuditTestChild { Property2 = "AAAAA1", Property3 = "BBBBB2" },
            new MyAuditTestChild { Property2 = "AAAAA2", Property3 = "BBBBB2" }
        };

        var data = new MyAuditTestParent
        {
            Ignore1 = "Test1",
            Property2 = "Test2",
            Property3 = "Test3",
            Null1 = null,
            ListIgnored = ["123", "456"],
            ListIncluded = ["789", "ABC"],
            ComplexListIncluded = myAuditTestChildArray1,
            DeepCollection =
            [
                new MyDeepAuditTestChild
                {
                    Property2 = "MyDeepAuditTestChild2",
                    Property3 = "MyDeepAuditTestChild3",
                    EmbeddedObject = new MyAuditTestChild
                    {
                        Property2 = "DDD",
                        Property3 = "DDD"
                    },
                    EmbeddedArray = myAuditTestChildArray2
                }
            ]
        };
        const string expected = "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParent\",\"Name\":\"Test2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}],\"PropertyNamesMapping\":{\"Name\":\"Some name for Property2\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestParentProperty3\":\"Description\",\"NullToBeHere\":\"NullToBeHere\",\"StringArray\":\"StringArray\",\"ObjectArray\":\"ObjectArray\",\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\",\"VeryDeepArray\":\"VeryDeepArray\",\"DeepChildName\":\"DeepChildName\",\"DeepChildDescription\":\"DeepChildDescription\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedObject\":\"EmbeddedObject\",\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyDeepAuditTestChildEmbeddedArray\":\"EmbeddedArray\"}}";
        yield return new TestCaseData(data, expected);

        yield return new TestCaseData("simple string value", "{\"RevisionType\":\"SystemString\",\"Value\":\"simple string value\",\"PropertyNamesMapping\":{}}");
        yield return new TestCaseData(new[] { "one", "two" }, "{\"RevisionType\":\"SystemString[]\",\"Values\":[\"one\",\"two\"],\"PropertyNamesMapping\":{}}");

        yield return new TestCaseData(myAuditTestChildArray1, "{\"RevisionType\":\"AiirhCommonLibrariesTestsAuditRevisionCreatorTestsMyAuditTestChild[]\",\"Values\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"PropertyNamesMapping\":{\"ChildName\":\"ChildName\",\"ChildDescription\":\"ChildDescription\"}}");
    }
}