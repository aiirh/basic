using System;

namespace Aiirh.DateAndTime
{
    public readonly struct Time : IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }

        private int TotalSeconds => (Hours * 3600) + (Minutes * 60);

        public Time(string time) : this(int.Parse(time[..2]), time.Length <= 3 ? 0 : int.Parse(time.Substring(time.Length - 2, 2)))
        {
        }

        public Time(DateTime dateTime) : this(dateTime.Hour, dateTime.Minute)
        {
        }

        public Time(int hours, int minutes)
        {
            Hours = (byte)hours;
            Minutes = (byte)minutes;
        }

        public DateTime ToDateTime(Date date)
        {
            var dateTime = date.ToDateTime();
            return dateTime.AddHours(Hours).AddMinutes(Minutes);
        }

        public Time AddMinutes(byte minutes)
        {
            var hoursToAdd = (byte)((Minutes + minutes) / 60);
            var hoursToSet = CalculateNewHours(hoursToAdd);
            var minutesToSet = CalculateNewMinutes(minutes);
            return new Time(hoursToSet, minutesToSet);
        }

        public Time AddHours(byte hours)
        {
            var newHours = (Hours + hours) % 24;
            return new Time(newHours, Minutes);
        }

        public Time Floor(byte precisionInMinutes)
        {
            var adjustedMinutes = (int)(Math.Floor(Minutes / (float)precisionInMinutes) * precisionInMinutes);
            return new Time(Hours, adjustedMinutes);
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

            return new Time(hoursToSet, minutesToSet);
        }

        private int CalculateNewHours(byte hours)
        {
            return (Hours + hours) % 24;
        }

        private int CalculateNewMinutes(byte minutes)
        {
            return (Minutes + minutes) % 60;
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
            return Hours == other.Hours && Minutes == other.Minutes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hours, Minutes);
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
            return $"{Hours:D2}{Minutes:D2}";
        }

        public int CompareTo(Time other)
        {
            var hoursComparison = Hours.CompareTo(other.Hours);
            return hoursComparison != 0 ? hoursComparison : Minutes.CompareTo(other.Minutes);
        }
    }
}
