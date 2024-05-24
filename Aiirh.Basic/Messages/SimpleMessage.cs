namespace Aiirh.Basic.Messages;

public class SimpleMessage : IMessage
{
    public string Header { get; private set; }

    public string Description { get; private set; }

    public bool IsSimpleMessage => Type == Type.Simple;

    public bool IsError => Type == Type.Error;

    public bool IsWarning => Type == Type.Warning;

    public bool IsWarningOrSimpleMessage => IsSimpleMessage || IsWarning;

    public Type Type { get; private set; }

    private SimpleMessage()
    {
    }

    public static SimpleMessage Simple(string header, string description)
    {
        return new SimpleMessage
        {
            Header = header,
            Description = description,
            Type = Type.Simple
        };
    }

    public static SimpleMessage Error(string header, string description)
    {
        return new SimpleMessage
        {
            Header = header,
            Description = description,
            Type = Type.Error
        };
    }

    public static SimpleMessage Validation(string header, string description, ValidationMessageSeverity severity)
    {
        return new SimpleMessage
        {
            Header = header,
            Description = description,
            Type = severity == ValidationMessageSeverity.Warning ? Type.Warning : Type.Error
        };
    }

    public static SimpleMessage CopyAndOverrideHeader(SimpleMessage source, string headerOverride)
    {
        if (string.IsNullOrWhiteSpace(headerOverride))
        {
            return source;
        }

        var newDescription = source.Header;
        return new SimpleMessage
        {
            Type = source.Type,
            Description = newDescription,
            Header = headerOverride
        };
    }

    public override string ToString()
    {
        return MessageBuilder.BuildMessage(Header, Description);
    }
}

internal static class MessageBuilder
{
    public static string BuildMessage(string header, string description)
    {
        var finalHeader = string.IsNullOrWhiteSpace(header) ? "Error" : header;
        var finalDescription = string.IsNullOrWhiteSpace(description) ? null : $": {description}";
        return $"{finalHeader}{finalDescription}";
    }
}