using Aiirh.Basic.Entities.Messages;

namespace Aiirh.Basic.Entities.Validation
{
    public class ValidationMessage
    {
        public string Header => WebMessage.Header;
        public string Description => WebMessage.Description;

        public WebMessage WebMessage { get; }

        public ValidationMessageSeverity Severity
        {
            get
            {
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

        public ValidationMessage(IMessage webMessage, ValidationMessageSeverity severity) : this(webMessage.Header, webMessage.Description, severity) { }

        public ValidationMessage(string message, string description, ValidationMessageSeverity severity)
        {
            WebMessage = WebMessage.Validation(message, description, severity);
        }

        public ValidationMessage(ValidationCheck check) : this(check.Message.Header, check.Message.Description, check.Message.Severity)
        {
        }
    }
}
