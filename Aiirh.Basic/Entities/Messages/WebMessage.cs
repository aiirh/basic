namespace Aiirh.Basic.Entities.Messages
{
    public class WebMessage : IMessage
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public bool IsSimpleError => Type == Type.Simple;
        public bool IsValidationError => Type == Type.ValidationError;
        public bool IsValidationWarning => Type == Type.ValidationWarning;
        public Type Type { get; set; }

        private WebMessage() { }

        public static WebMessage Simple(string header, string description)
        {
            return new WebMessage
            {
                Header = header,
                Description = description,
                Type = Type.Simple
            };
        }

        public static WebMessage Validation(string header, string description, ValidationMessageSeverity severity)
        {
            return new WebMessage
            {
                Header = header,
                Description = description,
                Type = severity == ValidationMessageSeverity.Warning ? Type.ValidationWarning : Type.ValidationError
            };
        }

        public static WebMessage CopyAndOverrideHeader(WebMessage source, string headerOverride)
        {
            if (string.IsNullOrWhiteSpace(headerOverride))
            {
                return source;
            }
            var newDescription = source.Header;
            return new WebMessage
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

    public static class MessageBuilder
    {
        public static string BuildMessage(string header, string description)
        {
            var finalHeader = string.IsNullOrWhiteSpace(header) ? "Error" : header;
            var finalDescription = string.IsNullOrWhiteSpace(description) ? null : $": {description}";
            return $"{finalHeader}{finalDescription}";
        }
    }
}
