using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Utilities;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Aiirh.Basic.Tests;

[TestFixture]
public class CollectionUtilityTests
{
    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void Shuffle(IEnumerable<int> data)
    {
        var dataList = data.ToList();
        var actual = dataList.Shuffle().ToList();
        ClassicAssert.IsTrue(dataList.CompareCollections(actual));
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData(Enumerable.Range(1, 10));
        yield return new TestCaseData(Enumerable.Range(6, 100));
        yield return new TestCaseData(Enumerable.Range(-100, 100));
        yield return new TestCaseData(Enumerable.Empty<int>());
        yield return new TestCaseData(Enumerable.Range(1, 1));
    }
}