using System;
using System.Linq;

namespace Aiirh.Basic.Entities.Time
{
    public static partial class SystemClock
    {
        public static DateTime Today => DateTime.Today;
        public static DateTime Now => DateTime.UtcNow;

        public static DateTime NowAtTimezone(TimeZones timeZone)
        {
            return TimeZoneInfo.ConvertTime(Now, FindTimeZone(timeZone));
        }

        /// <summary>
        /// Returns DateTime object with changed value of Hour property.
        /// E.g. if received 13:00 UTC time and convert to FLE (Tallinn, Helsinki) timezone, it will return 15:00
        /// </summary>
        /// <param name="dateTimeUtc"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime ConvertToTimezone(this DateTime dateTimeUtc, TimeZones timeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTimeUtc, FindTimeZone(timeZone));
        }

        /// <summary>
        /// Returns DateTime object with the same Hour property, but with specified UTC Offset.
        /// E.g. if received 13:00 of unspecified time and interpret as FLE (Tallinn, Helsinki) timezone,
        /// it will return still 13:00, but TimeSpan +2 hours will be added.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTimeOffset InterpretAsTimezone(this DateTime dateTime, TimeZones timeZone)
        {
            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
            var offset = FindTimeZone(timeZone).GetUtcOffset(dateTime);
            return new DateTimeOffset(dateTime, offset);
        }

        private static TimeZoneInfo FindTimeZone(TimeZones timeZone)
        {
            var timeZoneCode = SystemClock._timeZoneMapping[timeZone];
            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById(timeZoneCode);
            }
            catch
            {
                var allTimeZones = TimeZoneInfo.GetSystemTimeZones();
                var timeZoneSpanAgainstGmt = SystemClock._timeZoneMappingTimeSpans[timeZone];
                var gmtOffsetAgainstUtc = allTimeZones.First(x => x.Id == "GMT Standard Time").BaseUtcOffset;
                var timeZoneOffsetAgainstUtc = timeZoneSpanAgainstGmt + gmtOffsetAgainstUtc;
                var nearestTimezone = allTimeZones.FirstOrDefault(x => !x.SupportsDaylightSavingTime && x.BaseUtcOffset.Equals(timeZoneOffsetAgainstUtc));
                return nearestTimezone;
            }
        }

        public static string GetTimeZoneCode(TimeZones timeZone)
        {
            if (SystemClock._timeZoneMapping.TryGetValue(timeZone, out var timeZoneCode))
            {
                return timeZoneCode;
            }

            return null;
        }
    }
}