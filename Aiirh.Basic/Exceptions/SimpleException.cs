using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Messages;
using Aiirh.Basic.Utilities;
using Aiirh.Basic.Validation;

namespace Aiirh.Basic.Exceptions;

public class SimpleException : Exception
{
    public IEnumerable<SimpleMessage> Messages { get; }

    public SimpleException(IEnumerable<ValidationMessage> messages)
    {
            Messages = messages?.Select(x => x.Message) ?? Enumerable.Empty<SimpleMessage>();
        }

    public SimpleException(ValidationMessages messages) : this(messages.AsEnumerable()) { }

    private SimpleException(string header, string description, Exception innerException) : base(MessageBuilder.BuildMessage(header, description), innerException)
    {
            Messages = SimpleMessage.Error(header, description).MakeCollection();
        }

    public SimpleException(string header, string description) : this(header, description, null) { }

    public SimpleException(string header) : this(header, (string)null) { }

    public SimpleException(string header, Exception innerException) : this(header, null, innerException) { }

    public SimpleException(IOperationResult operationResult) : base(string.Join(";", operationResult.Messages.Select(x => MessageBuilder.BuildMessage(x.Header, x.Description))))
    {
            Messages = operationResult.Messages ?? Enumerable.Empty<SimpleMessage>();
        }

    public SimpleException(RequestResult requestResult)
    {
            Messages = requestResult.Messages ?? Enumerable.Empty<SimpleMessage>();
        }
}