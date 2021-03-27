using System;

namespace Aiirh.Basic.Time.Extensions
{
    public static class TimeExtensions
    {
        public static DateTime GetBeginningOfDay(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        public static DateTime GetEndOfDay(this DateTime dateTime)
        {
            return dateTime.Date.AddDays(1).AddTicks(-1); ;
        }
    }
}
