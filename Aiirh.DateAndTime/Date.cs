using System;
using System.Collections.Generic;

namespace Aiirh.DateAndTime;

public readonly struct Date : IComparable<Date>
{
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public Date(DateTime date)
    {
        Year = date.Year;
        Month = date.Month;
        Day = date.Day;
    }

    public Date(DateTime utcDateTime, IanaTimeZone timeZone)
    {
        var localTime = utcDateTime.ConvertToTimezone(timeZone);
        Year = localTime.Year;
        Month = localTime.Month;
        Day = localTime.Day;
    }

    public Date(string date) : this(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(5, 2)), int.Parse(date.Substring(8, 2)))
    {
    }

    public Date(int year, int month, int day)
    {
        Year = year;
        Month = month;
        Day = day;
    }

    public static Date Today(IanaTimeZone timeZone)
    {
        return new Date(SystemClock.Today, timeZone);
    }

    public bool IsToday(IanaTimeZone timeZone)
    {
        return Equals(Today(timeZone));
    }

    public DateTime ToDateTime()
    {
        return new DateTime(Year, Month, Day, 0, 0, 0, DateTimeKind.Unspecified);
    }

    public Date AddDays(int days)
    {
        var dateTime = ToDateTime();
        return new Date(dateTime.AddDays(days));
    }

    public DateTimeOffset ToDateTimeOffset(IanaTimeZone timeZone)
    {
        return ToDateTime().InterpretAsTimezone(timeZone);
    }

    public override bool Equals(object obj)
    {
        if (obj is Date date)
        {
            return Equals(date);
        }

        return false;
    }

    public bool Equals(Date other)
    {
        return Year == other.Year && Month == other.Month && Day == other.Day;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, Month, Day);
    }

    public static bool operator ==(Date a, Date b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Date a, Date b)
    {
        return !a.Equals(b);
    }

    public static bool operator <(Date a, Date b)
    {
        return a.ToDateTime() < b.ToDateTime();
    }

    public static bool operator >(Date a, Date b)
    {
        return a.ToDateTime() > b.ToDateTime();
    }

    public static bool operator <=(Date a, Date b)
    {
        return a < b || a == b;
    }

    public static bool operator >=(Date a, Date b)
    {
        return a > b || a == b;
    }

    public static Date operator ++(Date a)
    {
        return a.AddDays(1);
    }

    public override string ToString()
    {
        return $"{Year:D4}-{Month:D2}-{Day:D2}";
    }

    public int CompareTo(object obj)
    {
        return ToDateTime().CompareTo(((Date)obj).ToDateTime());
    }

    public int CompareTo(Date other)
    {
        var yearComparison = Year.CompareTo(other.Year);
        if (yearComparison != 0)
        {
            return yearComparison;
        }

        var monthComparison = Month.CompareTo(other.Month);
        return monthComparison != 0 ? monthComparison : Day.CompareTo(other.Day);
    }

    public static IEnumerable<Date> GetAllDatesInRange(Date first, Date second)
    {
        var begin = first < second ? first : second;
        var end = begin == first ? second : first;

        for (var current = begin; current <= end; current++)
        {
            yield return current;
        }
    }
}