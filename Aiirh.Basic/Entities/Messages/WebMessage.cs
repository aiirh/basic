namespace Aiirh.Basic.Entities.Messages
{
    public class WebMessage : IErrorMessage
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string ApiMessage { get; set; }
        public bool IsSimpleError => Type == Type.Simple;
        public bool IsValidationError => Type == Type.ValidationError;
        public bool IsValidationWarning => Type == Type.ValidationWarning;
        public Type Type { get; set; }

        private WebMessage() { }

        public static WebMessage Simple(string header, string description, string apiMessage)
        {
            return new WebMessage
            {
                Header = header,
                Description = description,
                ApiMessage = apiMessage,
                Type = Type.Simple
            };
        }

        public static WebMessage Simple(string header, string description)
        {
            return new WebMessage
            {
                Header = header,
                Description = description,
                ApiMessage = BuildApiMessage(header, description),
                Type = Type.Simple
            };
        }

        public static WebMessage Validation(string header, string description, ValidationMessageSeverity severity)
        {
            return new WebMessage
            {
                Header = header,
                Description = BuildApiMessage(header, description),
                Type = severity == ValidationMessageSeverity.Warning ? Type.ValidationWarning : Type.ValidationError
            };
        }

        public static WebMessage Validation(string header, string description, string apiMessage, ValidationMessageSeverity severity)
        {
            return new WebMessage
            {
                Header = header,
                Description = description,
                ApiMessage = apiMessage,
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
                Header = headerOverride,
                ApiMessage = BuildApiMessage(headerOverride, newDescription)
            };
        }

        public override string ToString()
        {
            return $"Header={Header}; Type={Type}; Description={Description}";
        }

        private static string BuildApiMessage(string header, string description)
        {
            return !string.IsNullOrWhiteSpace(description) ? $"{header}. {description}" : header;
        }
    }
}
