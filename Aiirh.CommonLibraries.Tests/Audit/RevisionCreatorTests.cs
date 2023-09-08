using Aiirh.Basic.Audit;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aiirh.CommonLibraries.Tests.Audit
{
    [TestFixture]
    public class RevisionCreatorTests
    {
        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void ToRevisionJson_ShouldReturnCorrectValue<T>(T data, string expected)
        {
            var result = data.ToRevisionJson();
            Assert.AreEqual(expected, result);
        }

        public class MyAuditTestParent
        {
            public string Ignore1 { get; set; }

            [Auditable("Name")]
            public string Property2 { get; set; }

            [Auditable("Description")]
            public string Property3 { get; set; }

            [Auditable("NullToBeHere")]
            public string Null1 { get; set; }

            public string[] ListIgnored { get; set; }

            [Auditable("StringArray")]
            public string[] ListIncluded { get; set; }

            [Auditable("ObjectArray")]
            public MyAuditTestChild[] ComplexListIncluded { get; set; }

            [Auditable("VeryDeepArray")]
            public MyDeepAuditTestChild[] DeepCollection { get; set; }
        }

        public class MyAuditTestChild
        {
            public string Ignore1 { get; set; }

            [Auditable("ChildName")]
            public string Property2 { get; set; }

            [Auditable("ChildDescription")]
            public string Property3 { get; set; }
        }

        public class MyDeepAuditTestChild
        {
            public string Ignore1 { get; set; }

            [Auditable("DeepChildName")]
            public string Property2 { get; set; }

            [Auditable("DeepChildDescription")]
            public string Property3 { get; set; }

            [Auditable("EmbeddedObject")]
            public MyAuditTestChild EmbeddedObject { get; set; }

            [Auditable("EmbeddedArray")]
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
                ListIgnored = new[] { "123", "456" },
                ListIncluded = new[] { "789", "ABC" },
                ComplexListIncluded = myAuditTestChildArray1,
                DeepCollection = new[]
                {
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
                }
            };
            const string expected = "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestParent\",\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            yield return new TestCaseData(data, expected);

            yield return new TestCaseData("simple string value", "{\"RevisionType\":\"System.String\",\"Value\":\"simple string value\"}");
            yield return new TestCaseData(new[] { "one", "two" }, "{\"RevisionType\":\"System.String[]\",\"Values\":[\"one\",\"two\"]}");

            yield return new TestCaseData(myAuditTestChildArray1, "{\"RevisionType\":\"Aiirh.CommonLibraries.Tests.Audit.RevisionCreatorTests+MyAuditTestChild[]\",\"Values\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}]}");
        }
    }
}
