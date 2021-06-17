using Aiirh.Basic.Messages;

namespace Aiirh.Basic.Validation
{
    public class ValidationMessage
    {
        public string Header => Message.Header;
        public string Description => Message.Description;

        public SimpleMessage Message { get; }

        public ValidationMessageSeverity Severity
        {
            get
            {
                switch (Message.Type)
                {
                    case Type.Simple:
                    case Type.Warning:
                        return ValidationMessageSeverity.Warning;
                    case Type.Error:
                    default:
                        return ValidationMessageSeverity.Error;
                }
            }
        }

        private ValidationMessage() { }

        public ValidationMessage(IMessage webMessage, ValidationMessageSeverity severity) : this(webMessage.Header, webMessage.Description, severity) { }

        public ValidationMessage(string message, string description, ValidationMessageSeverity severity)
        {
            Message = SimpleMessage.Validation(message, description, severity);
        }

        public ValidationMessage(ValidationCheck check) : this(check.Message.Header, check.Message.Description, check.Message.Severity)
        {
        }
    }
}
