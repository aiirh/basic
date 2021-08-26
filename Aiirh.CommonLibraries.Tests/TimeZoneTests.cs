using Aiirh.Basic.Time;
using NUnit.Framework;
using System;

namespace Aiirh.CommonLibraries.Tests
{
    [TestFixture]
    public class TimeZoneTests
    {

        [Test]
        public void ConvertToTimezone()
        {
            var testDate = new DateTime(2021, 08, 12, 15, 0, 0, DateTimeKind.Utc);
            var result = testDate.ConvertToTimezone(TimeZones.UTC);
        }

        [Test]
        public void GetTimeZone()
        {
            var timeZone = SystemClock.GetTimeZoneByCurrentOffset(180, new[] { TimeZones.FLEStandardTime });
        }
    }
}