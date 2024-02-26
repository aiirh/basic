using System.Collections.Generic;
using Aiirh.Basic.Utilities;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Aiirh.CommonLibraries.Tests;

[TestFixture]
public class StringUtilityTests
{
    [Test]
    [TestCaseSource(nameof(GetTestData))]
    public void ToSentence(string sourceStrung, string expected)
    {
        var actual = sourceStrung.ToSentence();
        ClassicAssert.AreEqual(expected, actual);
    }

    private static IEnumerable<TestCaseData> GetTestData()
    {
        yield return new TestCaseData("Lorem ipsum dolor sit amet", "Lorem ipsum dolor sit amet");
        yield return new TestCaseData("lorem ipsum dolor sit amet", "Lorem ipsum dolor sit amet");
        yield return new TestCaseData("LOREM IPSUM DOLOR SIT AMET", "Lorem ipsum dolor sit amet");
        yield return new TestCaseData("Lorem Ipsum Dolor Sit Amet", "Lorem ipsum dolor sit amet");
        yield return new TestCaseData("lOrEm iPsUm dOlOr sIt aMeT", "Lorem ipsum dolor sit amet");

        yield return new TestCaseData("Lorem, ipsum, dolor, sit, amet", "Lorem, ipsum, dolor, sit, amet");
        yield return new TestCaseData("Lorem ipsum. dolor. sit amet", "Lorem ipsum. Dolor. Sit amet");
        yield return new TestCaseData("Lorem ipsum.dolor.sit amet", "Lorem ipsum.dolor.sit amet");
    }
}