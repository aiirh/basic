using Aiirh.Basic.Audit;
using NUnit.Framework;

namespace Aiirh.CommonLibraries.Tests.Audit
{
    [TestFixture]
    public class RevisionCreatorTests
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Test()
        {
            var data = new MyAuditTestParent
            {
                Property1 = "Test1",
                Property2 = "Test2",
                Property3 = "Test3",
                Property4 = null,
                ListIgnored = new[] { "123", "456" },
                ListIncluded = new[] { "789", "ABC" },
                ComplexListIncluded = new[]
                {
                    new MyAuditTestChild
                    {
                        Property1 = "C1",
                        Property2 = "C2",
                        Property3 = "C3"
                    },
                    new MyAuditTestChild
                    {
                        Property1 = "C4",
                        Property2 = "C5",
                        Property3 = "C6"
                    }
                }
            };
            var result = RevisionCreator.CreateRevision(data);
        }

        public class MyAuditTestParent
        {
            public string Property1 { get; set; }

            [Auditable("Name")]
            public string Property2 { get; set; }

            [Auditable("Description")]
            public string Property3 { get; set; }

            [Auditable("NullToBeHere")]
            public string Property4 { get; set; }

            public string[] ListIgnored { get; set; }

            [Auditable("SimpleCollection")]
            public string[] ListIncluded { get; set; }

            [Auditable("ComplexCollection")]
            public MyAuditTestChild[] ComplexListIncluded { get; set; }
        }

        public class MyAuditTestChild
        {
            public string Property1 { get; set; }

            [Auditable("ChildName")]
            public string Property2 { get; set; }

            [Auditable("ChildDescription")]
            public string Property3 { get; set; }
        }
    }
}
