using Aiirh.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter;

namespace Aiirh.Basic.Time
{
    [Obsolete("Use Aiirh.DateAndTime package")]
    public static class SystemClock
    {
        public static DateTime Today => DateTime.UtcNow.Date;

        public static DateTime Now => DateTime.UtcNow;

        public static DateTime NowAtTimezone(TimeZones timeZone)
        {
            return TimeZoneInfo.ConvertTime(Now, FindTimeZone(timeZone));
        }

        public static DateTime TodayAtTimezone(TimeZones timeZone)
        {
            return NowAtTimezone(timeZone).Date;
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

        public static TimeZones GetTimeZoneByCurrentOffset(int offsetInMinutes, IEnumerable<TimeZones> preferredTimeZones = null)
        {
            var nowUtc = DateTime.UtcNow;
            var allTimeZonesCurrentOffsets = TimeZoneInfo.GetSystemTimeZones();
            var timeZonesThatMatch = allTimeZonesCurrentOffsets.Where(x => x.GetUtcOffset(nowUtc).TotalMinutes.Equals(offsetInMinutes)).Select(x => x.StandardName);
            var timeZones = TimeZoneMapping.TimeZoneToName.Where(x => timeZonesThatMatch.Contains(x.Value, StringComparer.InvariantCultureIgnoreCase)).Select(x => x.Key);
            var preferredTimeZonesList = preferredTimeZones?.ToList();
            var targetTimeZones = preferredTimeZones == null || !preferredTimeZonesList.Any() ? timeZones : timeZones.Intersect(preferredTimeZonesList);
            return targetTimeZones.FirstOrDefault();
        }

        private static TimeZoneInfo FindTimeZone(TimeZones timeZone)
        {
            var timeZoneCode = TimeZoneMapping.TimeZoneToName[timeZone];
            try
            {
                var timeZoneInfo = TZConvert.GetTimeZoneInfo(timeZoneCode);
                return timeZoneInfo;
            }
            catch
            {
                throw new SimpleException($"Timezone {timeZone} is not supported by the system");
            }
        }
    }
}