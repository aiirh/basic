using System;

namespace Aiirh.Basic.Time.Extensions
{
    public static class TimeExtensions
    {
        [Obsolete("Use Aiirh.DateAndTime package")]
        public static DateTime GetBeginningOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        [Obsolete("Use Aiirh.DateAndTime package")]
        public static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1);
        }
    }
}
