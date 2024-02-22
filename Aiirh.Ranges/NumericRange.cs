using System;
using Newtonsoft.Json;

namespace Aiirh.Ranges;

public class NumericRange : IComparable<NumericRange>
{
    private bool _isEmpty;

    public long Begin { get; set; }
    public long End { get; private set; }

    [JsonIgnore]
    public long Count => _isEmpty ? 0 : End - Begin + 1;

    [JsonConstructor]
    public NumericRange(long begin, long end)
    {
        Begin = begin <= end ? begin : end;
        End = begin <= end ? end : begin;
        _isEmpty = false;
    }

    public NumericRange(int single)
    {
        Begin = End = single;
        _isEmpty = false;
    }

    public override bool Equals(object obj)
    {
        return obj is NumericRange range && Begin == range.Begin && End == range.End;
    }

    public override int GetHashCode()
    {
        var hashCode = 1903003160;
        hashCode = hashCode * -1521134295 + Begin.GetHashCode();
        hashCode = hashCode * -1521134295 + End.GetHashCode();
        return hashCode;
    }

    public int CompareTo(NumericRange that)
    {
        if (Begin == that.Begin)
        {
            return End.CompareTo(that.End);
        }

        return Begin.CompareTo(that.Begin);
    }

    public long GetFirstAndRemove()
    {
        if (Count > 1)
        {
            return Begin++;
        }

        if (Count == 1)
        {
            _isEmpty = true;
            return Begin;
        }

        throw new ArgumentException("Range is empty");
    }

    public void Add(int value)
    {
        if (!CanBeAddedToHeadOrTail(value))
        {
            throw new ArgumentException("Value can't be added to this range");
        }

        if (_isEmpty)
        {
            Begin = End = value;
        }
        else
        {
            if (value == End + 1)
            {
                End = value;
            }

            if (value == Begin - 1)
            {
                Begin = value;
            }
        }

        _isEmpty = false;
    }

    public bool Contains(int value)
    {
        return !_isEmpty && value >= Begin && value <= End;
    }

    public bool CanBeAddedToHeadOrTail(int value)
    {
        return _isEmpty || !_isEmpty && (value == Begin - 1 || value == End + 1);
    }
}