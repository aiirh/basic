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
        protected bool? IsExplicitSuccess;

        public bool Success => IsExplicitSuccess ?? Messages?.All(x => x.IsSimpleMessage) ?? true;

        public IEnumerable<SimpleMessage> Messages { get; protected set; }

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool SuccessOrOnlyWarnings => IsExplicitSuccess ?? Messages?.All(x => x.IsWarningOrSimpleMessage) ?? true;

        public static RequestResult CreateFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult
            {
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult CreateError(Exception e)
        {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Error(e.Message, e.LogException()).MakeCollection();
            return new RequestResult
            {
                IsExplicitSuccess = false,
                Messages = messages
            };
        }

        public static RequestResult Create(IEnumerable<SimpleMessage> messages)
        {
            return new RequestResult
            {
                Messages = messages,
            };
        }

        public static RequestResult CreateError(IEnumerable<string> messages)
        {
            return Create(messages.Select(message => SimpleMessage.Error(message, null)));
        }

        public static RequestResult CreateError(string header, string description = null)
        {
            return Create(SimpleMessage.Error(header, description).MakeCollection());
        }

        public static RequestResult Create(SimpleMessage message)
        {
            return Create(message.MakeCollection());
        }

        public static RequestResult CreateValidation(string header, string description, ValidationMessageSeverity severity)
        {
            return Create(SimpleMessage.Validation(header, description, severity));
        }

        public static RequestResult CreateSuccess(string header = null, string description = null)
        {
            return new RequestResult
            {
                Messages = SimpleMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection(),
            };
        }
    }

    public class RequestResult<T> : RequestResult
    {

        public T Data { get; private set; }

        private RequestResult() { }

        public static RequestResult<T> Create(IEnumerable<SimpleMessage> messages, T data = default)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages
            };
        }

        public static RequestResult<T> Create(ValidationMessages messages, T data = default)
        {
            return Create(messages.Select(x => x.Message), data);
        }

        public static RequestResult<T> CreateSuccess(T data, string message = null)
        {
            return Create(SimpleMessage.Simple("Success!", message).MakeCollection(), data);
        }

        public static RequestResult<T> CreateError(IEnumerable<string> messages, T data = default)
        {
            return Create(messages.Select(message => SimpleMessage.Error(message, null)), data);
        }

        public static RequestResult<T> CreateError(string message, T data = default)
        {
            return CreateError(message.MakeCollection(), data);
        }

        public static RequestResult<T> CreateError(string header, string description, T data = default)
        {
            return Create(SimpleMessage.Error(header, description).MakeCollection(), data);
        }

        public static RequestResult<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default)
        {
            return Create(SimpleMessage.Validation(header, description, severity).MakeCollection(), data);
        }

        public static RequestResult<T> CreateValidation(ValidationMessages messages, T data = default)
        {
            return Create(messages, data);
        }


        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult)
        {
            return new RequestResult<T>
            {
                Data = default,
                IsExplicitSuccess = false,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride)
        {
            var newMessages = operationResult.Messages.Select(x => SimpleMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResult<T>
            {
                Data = default,
                IsExplicitSuccess = false,
                Messages = newMessages
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
        {
            return new RequestResult<T>
            {
                Messages = results.Where(x => !x.Success).SelectMany(x => x.Messages),
                Data = default,
                IsExplicitSuccess = false
            };
        }

        public static RequestResult<T> CreateFromOperationResult(IOperationResult<T> operationResult)
        {
            return new RequestResult<T>
            {
                Data = operationResult.Data,
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }

        public static RequestResult<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc)
        {
            return new RequestResult<T>
            {
                Data = mappingFunc(operationResult.Data),
                IsExplicitSuccess = operationResult.Success,
                Messages = operationResult.Messages
            };
        }
    }
}
