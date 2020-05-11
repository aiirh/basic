using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Validation;
using Aiirh.Basic.Extensions;

namespace Aiirh.Basic.Entities.Messages
{
    public class RequestResult
    {
        public bool Success { get; protected set; }
        public IEnumerable<WebMessage> Messages { get; protected set; }

        public static RequestResult CreateFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult
            {
                Success = operationResult.Success,
                Messages = operationResult.WebMessages
            };
        }

        public static RequestResult CreateError(string header, string description = null)
        {
            return new RequestResult
            {
                Messages = WebMessage.Simple(header, description, $"{header}. {description}").MakeCollection(),
                Success = false
            };
        }

        public static RequestResult CreateError(IEnumerable<WebMessage> messages)
        {
            return new RequestResult
            {
                Messages = messages
            };
        }

        public static RequestResult CreateError(WebMessage message)
        {
            return CreateError(message.MakeCollection());
        }

        public static RequestResult CreateValidation(string header, string description, ValidationMessageSeverity severity)
        {
            return CreateError(WebMessage.Validation(header, description, severity));
        }

        public static RequestResult CreateSuccess(string header = null, string description = null)
        {
            return new RequestResult
            {
                Messages = WebMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection(),
                Success = true
            };
        }
    }

    public class RequestResult<T> : RequestResult
    {
        public T Data { get; private set; }

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool HasValidationErrors => Messages?.Any(x => x.Type == Type.ValidationError) ?? false;

        private RequestResult() { }

        public static RequestResult<T> CreateSuccess(T data, ValidationMessages messages)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages.Select(x => x.WebMessage),
                Success = true
            };
        }

        public static RequestResult<T> CreateSuccess(T data, IEnumerable<WebMessage> messages)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages,
                Success = true
            };
        }

        public static RequestResult<T> CreateSuccess(T data, string message = null)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = WebMessage.Simple("Success!", message, message).MakeCollection(),
                Success = true
            };
        }

        public static RequestResult<T> CreateError(IEnumerable<string> messages, T data = default)
        {
            return CreateError(messages.Select(message => WebMessage.Simple(message, null, message)), data);
        }

        public static RequestResult<T> CreateError(string message, T data = default)
        {
            return CreateError(WebMessage.Simple(message, null, message), data);
        }

        public static RequestResult<T> CreateError(string header, string description, T data = default)
        {
            return CreateError(WebMessage.Simple(header, description, $"{header}. {description}"), data);
        }

        public static RequestResult<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default)
        {
            return CreateError(WebMessage.Validation(header, description, severity), data);
        }

        public static RequestResult<T> CreateValidation(ValidationMessages messages, T data = default)
        {
            return CreateError(messages.GetWebMessages(), data);
        }

        public static RequestResult<T> CreateError(WebMessage message, T data = default)
        {
            return CreateError(message.MakeCollection(), data);
        }

        public static RequestResult<T> CreateError(IEnumerable<WebMessage> messages, T data = default)
        {
            return new RequestResult<T>
            {
                Messages = messages,
                Data = data
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = operationResult.WebMessages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride)
        {
            var newMessages = operationResult.WebMessages.Select(x => WebMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = newMessages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResults(params IOperationResult[] operationResults)
        {
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = operationResults.Where(x => !x.Success).SelectMany(x => x.WebMessages)
            };
        }

        public static RequestResult<T> CreateFromOperationResult(IOperationResult<T> operationResult)
        {
            return new RequestResult<T>
            {
                Data = operationResult.Data,
                Success = operationResult.Success,
                Messages = operationResult.WebMessages
            };
        }

        public static RequestResult<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc)
        {
            return new RequestResult<T>
            {
                Data = mappingFunc(operationResult.Data),
                Success = operationResult.Success,
                Messages = operationResult.WebMessages
            };
        }
    }
}
