using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using Aiirh.Basic.Validation;

namespace Aiirh.Basic.Messages
{
    public class RequestResult
    {
        public bool Success { get; protected set; }

        public IEnumerable<SimpleMessage> Messages { get; protected set; }

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool HasValidationErrors => Messages?.Any(x => x.Type == Type.ValidationError) ?? false;

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool SuccessOrOnlyWarnings => Success || (Messages?.All(x => x.IsValidationWarning) ?? true);

        public static RequestResult CreateFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult
            {
                Success = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult CreateError(Exception e)
        {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Simple(e.Message, e.LogException()).MakeCollection();
            return new RequestResult
            {
                Success = false,
                Messages = messages
            };
        }

        public static RequestResult CreateError(IEnumerable<SimpleMessage> messages)
        {
            return new RequestResult
            {
                Messages = messages,
            };
        }

        public static RequestResult CreateError(IEnumerable<string> messages)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)));
        }

        public static RequestResult CreateError(string header, string description = null)
        {
            return CreateError(SimpleMessage.Simple(header, description).MakeCollection());
        }

        public static RequestResult CreateError(SimpleMessage message)
        {
            return CreateError(message.MakeCollection());
        }

        public static RequestResult CreateValidation(string header, string description, ValidationMessageSeverity severity)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity));
        }

        public static RequestResult CreateSuccess(string header = null, string description = null)
        {
            return new RequestResult
            {
                Messages = SimpleMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection(),
                Success = true
            };
        }
    }

    public class RequestResult<T> : RequestResult
    {

        public T Data { get; private set; }

        private RequestResult() { }

        public static RequestResult<T> CreateSuccess(T data, IEnumerable<SimpleMessage> messages)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages,
                Success = true
            };
        }

        public static RequestResult<T> CreateSuccess(T data, ValidationMessages messages)
        {
            return CreateSuccess(data, messages.Select(x => x.Message));
        }

        public static RequestResult<T> CreateSuccess(T data, string message = null)
        {
            return CreateSuccess(data, SimpleMessage.Simple("Success!", message).MakeCollection());
        }

        public static RequestResult<T> CreateError(IEnumerable<SimpleMessage> messages, T data = default)
        {
            return new RequestResult<T>
            {
                Messages = messages,
                Data = data
            };
        }

        public static RequestResult<T> CreateError(IEnumerable<string> messages, T data = default)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)), data);
        }

        public static RequestResult<T> CreateError(string message, T data = default)
        {
            return CreateError(SimpleMessage.Simple(message, null), data);
        }

        public static RequestResult<T> CreateError(string header, string description, T data = default)
        {
            return CreateError(SimpleMessage.Simple(header, description), data);
        }

        public static RequestResult<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity), data);
        }

        public static RequestResult<T> CreateValidation(ValidationMessages messages, T data = default)
        {
            return CreateError(messages.GetWebMessages(), data);
        }

        public static RequestResult<T> CreateError(SimpleMessage message, T data = default)
        {
            return CreateError(message.MakeCollection(), data);
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride)
        {
            var newMessages = operationResult.Messages.Select(x => SimpleMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = newMessages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
        {
            return new RequestResult<T>
            {
                Messages = results.Where(x => !x.Success).SelectMany(x => x.Messages),
                Data = default,
                Success = false
            };
        }

        public static RequestResult<T> CreateFromOperationResult(IOperationResult<T> operationResult)
        {
            return new RequestResult<T>
            {
                Data = operationResult.Data,
                Success = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc)
        {
            return new RequestResult<T>
            {
                Data = mappingFunc(operationResult.Data),
                Success = operationResult.Success,
                Messages = operationResult.Messages
            };
        }
    }
}
