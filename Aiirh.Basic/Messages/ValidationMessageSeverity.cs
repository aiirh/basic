namespace Aiirh.Basic.Messages
{
    public enum ValidationMessageSeverity
    {
        Error,
        Warning
    }

    public enum Type
    {
        Simple = 0,
        ValidationError = 10,
        ValidationWarning = 20
    }
}
