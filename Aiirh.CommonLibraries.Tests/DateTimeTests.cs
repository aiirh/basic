using Aiirh.DateAndTime;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;

namespace Aiirh.CommonLibraries.Tests;

[TestFixture]
public class DateTimeTests
{
    [Test]
    [TestCaseSource(nameof(GetTestCasesTimeCreate))]
    public void TimeCreate(string testData, Time expected)
    {
        var actual = new Time(testData);
        ClassicAssert.AreEqual(expected, actual);
    }

    [Test]
    [TestCaseSource(nameof(GetTestCasesTimeFormat))]
    public void TimeFormat(Time testData, string format, string expected)
    {
        var actual = testData.Format(format);
        ClassicAssert.AreEqual(expected, actual);
    }

    [Test]
    [TestCaseSource(nameof(GetTestCasesAddMinutes))]
    public void TimeFormat(Time testData, int minutes, Time expected)
    {
        var actual = testData.AddMinutes(minutes);
        ClassicAssert.AreEqual(expected, actual);
    }

    private static IEnumerable<TestCaseData> GetTestCasesTimeCreate()
    {
        yield return new TestCaseData("00", new Time(0, 0, 0));
        yield return new TestCaseData("06", new Time(6, 0, 0));
        yield return new TestCaseData("1210", new Time(12, 10, 0));
        yield return new TestCaseData("00:01:02", new Time(0, 1, 2));
        yield return new TestCaseData("23:59:59", new Time(23, 59, 59));
        yield return new TestCaseData("12:00:34", new Time(12, 0, 34));
        yield return new TestCaseData("120034", new Time(12, 0, 34));
        yield return new TestCaseData("1:2LSDs0as034", new Time(12, 0, 34));
    }

    private static IEnumerable<TestCaseData> GetTestCasesTimeFormat()
    {
        yield return new TestCaseData(new Time(11, 30, 13), "HH:mm:ss", "11:30:13");
        yield return new TestCaseData(new Time(11, 30, 13), "HHmmss", "113013");
        yield return new TestCaseData(new Time(4, 2, 5), "HH:mm:ss", "04:02:05");
        yield return new TestCaseData(new Time(0, 0, 0), "TestMMmmHHhhSSss", "TestMM0000hhSS00");
        yield return new TestCaseData(new Time(1, 2, 3), "TestMMmmHHhhSSss", "TestMM0201hhSS03");
    }

    private static IEnumerable<TestCaseData> GetTestCasesAddMinutes()
    {
        yield return new TestCaseData(new Time(0, 1, 2), 2, new Time(0, 3, 2));
        yield return new TestCaseData(new Time(0, 59, 2), 2, new Time(1, 1, 2));
        yield return new TestCaseData(new Time(0, 1, 2), -2, new Time(23, 59, 2));
    }
}