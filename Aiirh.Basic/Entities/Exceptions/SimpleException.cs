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

        public IEnumerable<SimpleMessage> Messages { get; }

        public SimpleException(IEnumerable<ValidationMessage> messages)
        {
            Messages = messages?.Select(x => x.Message) ?? Enumerable.Empty<SimpleMessage>();
        }

        public SimpleException(ValidationMessages messages) : this(messages.AsEnumerable()) { }

        private SimpleException(string header, string description, Type exceptionType, Exception innerException) : base(MessageBuilder.BuildMessage(header, description), innerException)
        {
            switch (exceptionType)
            {
                case Type.ValidationError:
                    Messages = SimpleMessage.Validation(header, description, ValidationMessageSeverity.Error).MakeCollection();
                    break;
                case Type.ValidationWarning:
                    Messages = SimpleMessage.Validation(header, description, ValidationMessageSeverity.Warning).MakeCollection();
                    break;
                case Type.Simple:
                default:
                    Messages = SimpleMessage.Simple(header, description).MakeCollection();
                    break;
            }
        }

        public SimpleException(string header, string description, Type exceptionType) : this(header, description, exceptionType, null) { }
        public SimpleException(string header, string description) : this(header, description, Type.Simple) { }
        public SimpleException(string header) : this(header, (string)null) { }
        public SimpleException(string header, Exception innerException) : this(header, null, Type.Simple, innerException) { }

        public SimpleException(IOperationResult operationResult) : base(string.Join(";", operationResult.Messages.Select(x => MessageBuilder.BuildMessage(x.Header, x.Description))))
        {
            Messages = operationResult.Messages ?? Enumerable.Empty<SimpleMessage>();
        }

        public SimpleException(RequestResult requestResult)
        {
            Messages = requestResult.Messages ?? Enumerable.Empty<SimpleMessage>();
        }
    }
}
