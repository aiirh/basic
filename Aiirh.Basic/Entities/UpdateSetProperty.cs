namespace Aiirh.Basic.Entities;

public class UpdateSetProperty<T>
{
    internal bool UpdateIfNull;

    public T Value { get; set; }

    public UpdateSetProperty(T value, bool updateIfNull = false)
    {
            Value = value;
            UpdateIfNull = updateIfNull;
        }
}