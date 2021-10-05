using Aiirh.DateAndTime;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter;

namespace Aiirh.CommonLibraries.Tests
{
    [TestFixture]
    public class TimeZoneTests
    {
        [Test]
        [TestCaseSource(nameof(GetTestCasesConvertToTimezone))]
        public void ConvertToTimezone(DateTime testCase, IanaTimeZone timeZone, string expected, string testId)
        {
            var actual = testCase.ConvertToTimezone(timeZone).ToString("s");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTimeZoneList()
        {
            var allTimeZones = Enum.GetValues(typeof(IanaTimeZone)).Cast<IanaTimeZone>().ToList();
            var mappedCodes = allTimeZones.Select(x => TimeZoneMapping.TimeZoneToIanaName[x]);
            var allPossibleCodes = TZConvert.KnownIanaTimeZoneNames;
            var unknownCodesExist = mappedCodes.Any(x => !allPossibleCodes.Contains(x));
            Assert.IsFalse(unknownCodesExist);
        }

        private static IEnumerable<TestCaseData> GetTestCasesConvertToTimezone()
        {
            yield return new TestCaseData(new DateTime(2021, 10, 01, 15, 0, 0, DateTimeKind.Utc), IanaTimeZone.EuropeTallinn, "2021-10-01T18:00:00", "AED59FA3-957A-463D-BDC0-E8EDF28DF90D");
            yield return new TestCaseData(new DateTime(2021, 12, 01, 15, 0, 0, DateTimeKind.Utc), IanaTimeZone.EuropeTallinn, "2021-12-01T17:00:00", "D137501D-FAAA-4C56-B322-7BC64E940286");
        }
    }
}