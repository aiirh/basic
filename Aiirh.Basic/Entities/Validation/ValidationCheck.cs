using Aiirh.Basic.Entities.Messages;

namespace Aiirh.Basic.Entities.Validation
{
    public class ValidationCheck
    {

        public bool IsValid { get; private set; }
        public ValidationMessage Message { get; private set; }
        public ValidationMessageSeverity Severity { get; private set; }

        private ValidationCheck() { }

        public static ValidationCheck Create(bool isValid, string webMessageHeader, string webMessageDescription, string apiMessage, ValidationMessageSeverity severity)
        {
            return new ValidationCheck
            {
                IsValid = isValid,
                Message = new ValidationMessage(webMessageHeader, webMessageDescription, apiMessage, severity),
                Severity = severity
            };
        }

        public static ValidationCheck Create(bool isValid, string webMessageHeader, string webMessageDescription, ValidationMessageSeverity severity)
        {
            return new ValidationCheck
            {
                IsValid = isValid,
                Message = new ValidationMessage(webMessageHeader, webMessageDescription, null, severity),
                Severity = severity
            };
        }
    }
}
