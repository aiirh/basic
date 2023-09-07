using Aiirh.Basic.Audit;
using NUnit.Framework;

namespace Aiirh.CommonLibraries.Tests.Audit
{
    [TestFixture]
    public class AuditLogTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [Test]
        public void Test()
        {
            var json3 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\", \"YYY\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"C2\",\"Description\":\"C3\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            var json4 = "{\"Name\":\"Test2\",\"Description\":\"Test4\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\", \"XXX\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"YY\",\"Description\":\"Yyyy\"},{\"Name\":\"C2\",\"Description\":\"C9\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            var result2 = AuditLogBuilder.Build(json3, json4);
            var result3 = AuditLogBuilder.Build(json4, json3);

        }
    }
}
