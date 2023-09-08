using Aiirh.Basic.Audit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aiirh.CommonLibraries.Tests.Audit
{
    [TestFixture]
    public class AuditLogBuilderTests
    {
        [Test]
        [TestCaseSource(nameof(GetTestData))]
        public void AuditLogBuild_ShouldReturnCorrectValue(string json1, string json2, int countOfResults)
        {
            var result = AuditLogBuilder.Build(json1, json2, DateTime.Today, "Test author");
            Assert.AreEqual(countOfResults, result.Changes.Count());
        }

        private static IEnumerable<TestCaseData> GetTestData()
        {
            const string json1 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json2 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            yield return new TestCaseData(json1, json2, 0);
            yield return new TestCaseData(json2, json1, 0);

            const string json3 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\",\"YYY\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"C2\",\"Description\":\"C3\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            const string json4 = "{\"Name\":\"Test2\",\"Description\":\"Test4\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\",\"XXX\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"YY\",\"Description\":\"Yyyy\"},{\"Name\":\"C2\",\"Description\":\"C9\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            yield return new TestCaseData(json3, json4, 6);
            yield return new TestCaseData(json4, json3, 6);

            const string json5 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD1\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            const string json6 = "{\"Name\":\"Test1\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"StringArray\":[\"789\",\"ABC\"],\"ObjectArray\":[{\"ChildName\":\"C2\",\"ChildDescription\":\"C3\"},{\"ChildName\":\"C5\",\"ChildDescription\":\"C6\"}],\"VeryDeepArray\":[{\"DeepChildName\":\"MyDeepAuditTestChild2\",\"DeepChildDescription\":\"MyDeepAuditTestChild3\",\"EmbeddedObject\":{\"ChildName\":\"DDD2\",\"ChildDescription\":\"DDD\"},\"EmbeddedArray\":[{\"ChildName\":\"XXXXX1\",\"ChildDescription\":\"YYYYYY2\"},{\"ChildName\":\"AAAAA1\",\"ChildDescription\":\"BBBBB2\"},{\"ChildName\":\"AAAAA2\",\"ChildDescription\":\"BBBBB2\"}]}]}";
            yield return new TestCaseData(json5, json6, 4);
            yield return new TestCaseData(json6, json5, 4);

            const string json7 = "{\"Type\":\"System.String[]\",\"Values\":[\"one\",\"two\"]}";
            const string json8 = "{\"Type\":\"System.String[]\",\"Values\":[\"one\",\"three\"]}";
            yield return new TestCaseData(json7, json8, 1);
            yield return new TestCaseData(json8, json7, 1);

            const string json9 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"C2\",\"Description\":\"C3\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            const string json10 = "{\"Name\":\"Test2\",\"Description\":\"Test4\",\"NullToBeHere\":null,\"SimpleCollection\":[\"789\",\"ABC\",\"XXX\"],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"YY\",\"Description\":\"Yyyy\"},{\"Name\":\"C2\",\"Description\":\"C9\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            yield return new TestCaseData(json9, json10, 6);
            yield return new TestCaseData(json10, json9, 6);

            const string json11 = "{\"Name\":\"Test2\",\"Description\":\"Test3\",\"NullToBeHere\":null,\"SimpleCollection\":[789,567],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"C2\",\"Description\":\"C3\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            const string json12 = "{\"Name\":\"Test2\",\"Description\":\"Test4\",\"NullToBeHere\":null,\"SimpleCollection\":[789,567,112],\"ComplexCollection\":[{\"Name\":\"C0\",\"Description\":\"D0\"},{\"Name\":\"YY\",\"Description\":\"Yyyy\"},{\"Name\":\"C2\",\"Description\":\"C9\"},{\"Name\":\"C5\",\"Description\":\"C6\"}]}";
            yield return new TestCaseData(json11, json12, 6);
            yield return new TestCaseData(json12, json11, 6);
        }
    }
}
