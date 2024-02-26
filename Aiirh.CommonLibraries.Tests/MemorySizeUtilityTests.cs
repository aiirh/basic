using Aiirh.Basic.Utilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework.Legacy;

namespace Aiirh.CommonLibraries.Tests;

[TestFixture]
public class MemorySizeUtilityTests
{
    [Test]
    [TestCaseSource(nameof(GetTestCases))]
    public void EncryptAndDecrypt(int value, MemorySizeUnits units, string expected)
    {
        var actual = value.ToMemorySizeString(units);
        ClassicAssert.AreEqual(expected, actual);
    }

    private static IEnumerable<TestCaseData> GetTestCases()
    {
        var separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        yield return new TestCaseData(0, MemorySizeUnits.Decimal, "0 B");
        yield return new TestCaseData(1, MemorySizeUnits.Decimal, "1 B");
        yield return new TestCaseData(899, MemorySizeUnits.Decimal, "899 B");
        yield return new TestCaseData(901, MemorySizeUnits.Decimal, $"0{separator}9 KB");
        yield return new TestCaseData(950, MemorySizeUnits.Decimal, $"0{separator}95 KB");
        yield return new TestCaseData(1000, MemorySizeUnits.Decimal, "1 KB");

        yield return new TestCaseData(0, MemorySizeUnits.Binary, "0 B");
        yield return new TestCaseData(1, MemorySizeUnits.Binary, "1 B");
        yield return new TestCaseData(1000, MemorySizeUnits.Binary, $"0{separator}98 KiB");
        yield return new TestCaseData(1024, MemorySizeUnits.Binary, "1 KiB");
        yield return new TestCaseData(1040, MemorySizeUnits.Binary, $"1{separator}02 KiB");
    }
}