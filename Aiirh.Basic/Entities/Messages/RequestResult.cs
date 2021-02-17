using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Exceptions;
using Aiirh.Basic.Entities.Sounds;
using Aiirh.Basic.Entities.Validation;
using Aiirh.Basic.Extensions;

namespace Aiirh.Basic.Entities.Messages
{
    public class RequestResult
    {
        public bool Success { get; protected set; }
        public SoundSequence Sounds { get; protected set; }
        public IEnumerable<SimpleMessage> Messages { get; protected set; }

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool HasValidationErrors => Messages?.Any(x => x.Type == Type.ValidationError) ?? false;

        /// <summary>
        /// Used in frontend
        /// </summary>
        public bool SuccessOrOnlyWarnings => Success || (Messages?.All(x => x.IsValidationWarning) ?? true);

        public static RequestResult CreateFromOperationResult(IOperationResult operationResult, SoundSequence sounds = null)
        {
            return new RequestResult
            {
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }

        public static RequestResult CreateError(Exception e, SoundSequence sounds = null)
        {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Simple(e.Message, e.LogException()).MakeCollection();
            return new RequestResult
            {
                Success = false,
                Messages = messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResult CreateError(IEnumerable<SimpleMessage> messages, SoundSequence sounds = null)
        {
            return new RequestResult
            {
                Messages = messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResult CreateError(IEnumerable<string> messages, SoundSequence sounds = null)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)), sounds);
        }

        public static RequestResult CreateError(string header, string description = null, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(header, description).MakeCollection(), sounds);
        }

        public static RequestResult CreateError(SimpleMessage message, SoundSequence sounds = null)
        {
            return CreateError(message.MakeCollection(), sounds);
        }

        public static RequestResult CreateValidation(string header, string description, ValidationMessageSeverity severity, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity), sounds);
        }

        public static RequestResult CreateSuccess(string header = null, string description = null, SoundSequence sounds = null)
        {
            return new RequestResult
            {
                Messages = SimpleMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection(),
                Success = true,
                Sounds = sounds ?? SoundsCollection.Success
            };
        }
    }

    public class RequestResult<T> : RequestResult
    {

        public T Data { get; private set; }

        private RequestResult() { }

        public static RequestResult<T> CreateSuccess(T data, IEnumerable<SimpleMessage> messages, SoundSequence sounds = null)
        {
            return new RequestResult<T>
            {
                Data = data,
                Messages = messages,
                Success = true,
                Sounds = sounds ?? SoundsCollection.Success
            };
        }

        public static RequestResult<T> CreateSuccess(T data, ValidationMessages messages, SoundSequence sounds = null)
        {
            return CreateSuccess(data, messages.Select(x => x.Message), sounds);
        }

        public static RequestResult<T> CreateSuccess(T data, SoundSequence sounds)
        {
            return CreateSuccess(data, (string)null, sounds);
        }

        public static RequestResult<T> CreateSuccess(T data, string message = null, SoundSequence sounds = null)
        {
            return CreateSuccess(data, SimpleMessage.Simple("Success!", message).MakeCollection(), sounds);
        }

        public static RequestResult<T> CreateError(IEnumerable<SimpleMessage> messages, T data = default, SoundSequence sounds = null)
        {
            return new RequestResult<T>
            {
                Messages = messages,
                Data = data,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResult<T> CreateError(IEnumerable<string> messages, T data = default, SoundSequence sounds = null)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)), data, sounds);
        }

        public static RequestResult<T> CreateError(string message, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(message, null), data, sounds);
        }

        public static RequestResult<T> CreateError(string header, string description, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(header, description), data, sounds);
        }

        public static RequestResult<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity), data, sounds);
        }

        public static RequestResult<T> CreateValidation(ValidationMessages messages, T data = default, SoundSequence sounds = null)
        {
            return CreateError(messages.GetWebMessages(), data, sounds);
        }

        public static RequestResult<T> CreateError(SimpleMessage message, T data = default, SoundSequence sounds = null)
        {
            return CreateError(message.MakeCollection(), data, sounds);
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, SoundSequence sounds = null)
        {
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = operationResult.Messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResult<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride, SoundSequence sounds = null)
        {
            var newMessages = operationResult.Messages.Select(x => SimpleMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResult<T>
            {
                Data = default,
                Success = false,
                Messages = newMessages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResult<T> CreateFromOperationResult(IOperationResult<T> operationResult, SoundSequence sounds = null)
        {
            return new RequestResult<T>
            {
                Data = operationResult.Data,
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }

        public static RequestResult<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc, SoundSequence sounds = null)
        {
            return new RequestResult<T>
            {
                Data = mappingFunc(operationResult.Data),
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }
    }
}
