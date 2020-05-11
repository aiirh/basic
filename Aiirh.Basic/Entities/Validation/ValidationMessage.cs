using Aiirh.Basic.Entities.Messages;

namespace Aiirh.Basic.Entities.Validation
{
    public class ValidationMessage
    {

        public string Message => WebMessage.Header;
        public string Description => WebMessage.Description;
        public string ApiMessage => WebMessage.ApiMessage;
        public WebMessage WebMessage { get; }

        public ValidationMessageSeverity Severity {
            get {
                switch (WebMessage.Type)
                {
                    case Type.ValidationWarning:
                        return ValidationMessageSeverity.Warning;
                    case Type.ValidationError:
                    case Type.Simple:
                    default:
                        return ValidationMessageSeverity.Error;
                }
            }
        }

        private ValidationMessage() { }

        public ValidationMessage(WebMessage webMessage, ValidationMessageSeverity severity) : this(webMessage.Header, webMessage.Description, webMessage.ApiMessage, severity) { }

        public ValidationMessage(string message, string description, string apiMessage, ValidationMessageSeverity severity)
        {
            var resultApiMessage = string.IsNullOrWhiteSpace(apiMessage) ? $"{message}{(string.IsNullOrWhiteSpace(description) ? null : $": {description}")}" : apiMessage;
            WebMessage = WebMessage.Validation(message, description, resultApiMessage, severity);
        }

        public ValidationMessage(ValidationCheck check) : this(check.Message.Message, check.Message.Description, check.Message.ApiMessage, check.Severity)
        {
        }
    }
}
