using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Messages;
using Aiirh.Basic.Entities.Validation;
using Aiirh.Basic.Extensions;
using Type = Aiirh.Basic.Entities.Messages.Type;

namespace Aiirh.Basic.Entities.Exceptions
{
    public class SimpleException : Exception
    {

        public IEnumerable<WebMessage> WebMessages { get; }

        public SimpleException(IEnumerable<ValidationMessage> messages)
        {
            WebMessages = messages?.Select(x => x.WebMessage) ?? Enumerable.Empty<WebMessage>();
        }

        public SimpleException(ValidationMessages messages) : this(messages.AsEnumerable()) { }

        private SimpleException(string header, string description, Type exceptionType, Exception innerException) : base(MessageBuilder.BuildMessage(header, description), innerException)
        {
            switch (exceptionType)
            {
                case Type.ValidationError:
                    WebMessages = WebMessage.Validation(header, description, ValidationMessageSeverity.Error).MakeCollection();
                    break;
                case Type.ValidationWarning:
                    WebMessages = WebMessage.Validation(header, description, ValidationMessageSeverity.Warning).MakeCollection();
                    break;
                case Type.Simple:
                default:
                    WebMessages = WebMessage.Simple(header, description).MakeCollection();
                    break;
            }
        }

        public SimpleException(string header, string description, Type exceptionType) : this(header, description, exceptionType, null) { }
        public SimpleException(string header, string description) : this(header, description, Type.Simple) { }
        public SimpleException(string header) : this(header, (string)null) { }
        public SimpleException(string header, Exception innerException) : this(header, null, Type.Simple, innerException) { }

        public SimpleException(IOperationResult operationResult) : base(operationResult.SimpleMessage)
        {
            WebMessages = operationResult.WebMessages ?? Enumerable.Empty<WebMessage>();
        }

        public SimpleException(RequestResult requestResult)
        {
            WebMessages = requestResult.Messages ?? Enumerable.Empty<WebMessage>();
        }
    }
}
