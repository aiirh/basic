using System;
using System.Linq;
using System.Text;

namespace Aiirh.DateAndTime;

public readonly struct Time : IComparable<Time>
{
    public byte Hours { get; }
    public byte Minutes { get; }
    public byte Seconds { get; }

    private int TotalSeconds => (Hours * 3600) + (Minutes * 60) + Seconds;

    public Time(string time)
    {
        var numericTime = new string(time.Where(char.IsDigit).ToArray());
        Hours = byte.Parse(numericTime[..2]);

        switch (numericTime.Length)
        {
            case 2:
                Minutes = 0;
                Seconds = 0;
                return;
            default:
                Minutes = byte.Parse(numericTime.Substring(2, 2));
                Seconds = numericTime.Length switch
                {
                    4 => 0,
                    6 => byte.Parse(numericTime.Substring(4, 2)),
                    _ => throw new FormatException($@"Value ""{time}"" can't be parsed as a Time struct")
                };
                return;
        }
    }

    public Time(DateTime dateTime) : this(dateTime.Hour, dateTime.Minute, dateTime.Second)
    {
    }

    public Time(int hours, int minutes, int seconds)
    {
        Hours = (byte)hours;
        Minutes = (byte)minutes;
        Seconds = (byte)seconds;
    }

    public DateTime ToDateTime(Date date)
    {
        var dateTime = date.ToDateTime();
        return dateTime.AddHours(Hours).AddMinutes(Minutes).AddSeconds(Seconds);
    }

    public Time AddMinutes(int minutes)
    {
        var dateTime = new DateTime(1970, 1, 1, Hours, Minutes, Seconds, DateTimeKind.Utc);
        var result = dateTime.AddMinutes(minutes);
        return new Time(result.Hour, result.Minute, result.Second);
    }

    public Time AddHours(int hours)
    {
        var dateTime = new DateTime(1970, 1, 1, Hours, Minutes, Seconds, DateTimeKind.Utc);
        var result = dateTime.AddHours(hours);
        return new Time(result.Hour, result.Minute, result.Second);
    }

    public Time Floor(byte precisionInMinutes)
    {
        var adjustedMinutes = (int)(Math.Floor(Minutes / (float)precisionInMinutes) * precisionInMinutes);
        return new Time(Hours, adjustedMinutes, 0);
    }

    public Time Ceiling(byte precisionInMinutes)
    {
        int hoursToSet;
        int minutesToSet;
        var adjustedMinutes = (int)(Math.Ceiling(Minutes / (float)precisionInMinutes) * precisionInMinutes);
        if (adjustedMinutes == 60)
        {
            hoursToSet = CalculateNewHours(1);
            minutesToSet = 0;
        }
        else
        {
            hoursToSet = Hours;
            minutesToSet = adjustedMinutes;
        }

        return new Time(hoursToSet, minutesToSet, 0);
    }

    private int CalculateNewHours(byte hours)
    {
        return (Hours + hours) % 24;
    }

    public override bool Equals(object obj)
    {
        if (obj is Time time)
        {
            return Equals(time);
        }

        return false;
    }

    public bool Equals(Time other)
    {
        return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Hours, Minutes, Seconds);
    }

    public static bool operator ==(Time a, Time b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Time a, Time b)
    {
        return !a.Equals(b);
    }

    public static bool operator <(Time a, Time b)
    {
        return a.TotalSeconds < b.TotalSeconds;
    }

    public static bool operator >(Time a, Time b)
    {
        return a.TotalSeconds > b.TotalSeconds;
    }

    public static bool operator <=(Time a, Time b)
    {
        return a < b || a == b;
    }

    public static bool operator >=(Time a, Time b)
    {
        return a > b || a == b;
    }

    public override string ToString()
    {
        return $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";
    }

    /// <summary>
    /// Returns string representation of a Time struct. Default value is HH:mm:ss
    /// </summary>
    /// <param name="format">Possible placeholders: HH - hours, mm - minutes, ss - seconds.</param>
    /// <returns></returns>
    public string Format(string format = "HH:mm:ss")
    {
        var result = new StringBuilder(format);
        result = result.Replace("HH", $"{Hours:D2}");
        result = result.Replace("mm", $"{Minutes:D2}");
        result = result.Replace("ss", $"{Seconds:D2}");
        return result.ToString();
    }

    public int CompareTo(Time other)
    {
        var hoursComparison = Hours.CompareTo(other.Hours);
        return hoursComparison != 0 ? hoursComparison : Minutes.CompareTo(other.Minutes);
    }
}