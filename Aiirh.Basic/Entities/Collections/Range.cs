using System;
using Newtonsoft.Json;

namespace Aiirh.Basic.Entities.Collections
{
    public class Range : IComparable<Range>
    {

        private bool _isEmpty;

        public int Begin { get; private set; }
        public int End { get; private set; }

        [JsonIgnore]
        public int Count => _isEmpty ? 0 : End - Begin + 1;

        [JsonConstructor]
        public Range(int begin, int end)
        {
            Begin = begin <= end ? begin : end;
            End = begin <= end ? end : begin;
            _isEmpty = false;
        }

        public Range(int single)
        {
            Begin = End = single;
            _isEmpty = false;
        }

        public override bool Equals(object obj)
        {
            return obj is Range range && Begin == range.Begin && End == range.End;
        }

        public override int GetHashCode()
        {
            var hashCode = 1903003160;
            hashCode = hashCode * -1521134295 + Begin.GetHashCode();
            hashCode = hashCode * -1521134295 + End.GetHashCode();
            return hashCode;
        }

        public int CompareTo(Range that)
        {

            if (Begin == that.Begin)
            {
                return End - that.End;
            }

            return Begin - that.Begin;
        }

        public int GetFirstAndRemove()
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
}
