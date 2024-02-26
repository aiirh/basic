namespace Aiirh.Basic.Entities;

public class FilterProperty<T>
{
    internal EmptyFilterValueBehavior EmptyFilterValueBehavior;

    public T Value { get; }

    public FilterProperty(T value, EmptyFilterValueBehavior emptyFilterValueBehavior = EmptyFilterValueBehavior.Ignore)
    {
            Value = value;
            EmptyFilterValueBehavior = emptyFilterValueBehavior;
        }
}

public enum EmptyFilterValueBehavior
{
    Ignore = 0,
    Filter = 1
}