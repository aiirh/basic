namespace Aiirh.Basic.Collections
{
    public class Range<T>
    {
        public T Begin => _range.Begin.GetValueFromLong<T>();

        public T End => _range.End.GetValueFromLong<T>();

        private readonly NumericRange _range;

        public Range(T begin, T end)
        {
            var beginLong = begin.GetLongRepresentation();
            var endLong = end.GetLongRepresentation();
            _range = new NumericRange(beginLong, endLong);
        }

        public NumericRange GetNumericRange()
        {
            return _range;
        }
    }
}
