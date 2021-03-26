using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Exceptions;
using Aiirh.Basic.Entities.Sounds;
using Aiirh.Basic.Entities.Validation;
using Aiirh.Basic.Extensions;

namespace Aiirh.Basic.Entities.Messages
{
    public class RequestResultWithSound : RequestResult
    {
        public SoundSequence Sounds { get; protected set; }

        public static RequestResultWithSound CreateFromOperationResult(IOperationResult operationResult, SoundSequence sounds = null)
        {
            return new RequestResultWithSound
            {
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }

        public static RequestResultWithSound CreateError(Exception e, SoundSequence sounds = null)
        {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Simple(e.Message, e.LogException()).MakeCollection();
            return new RequestResultWithSound
            {
                Success = false,
                Messages = messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResultWithSound CreateError(IEnumerable<SimpleMessage> messages, SoundSequence sounds = null)
        {
            return new RequestResultWithSound
            {
                Messages = messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResultWithSound CreateError(IEnumerable<string> messages, SoundSequence sounds = null)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)), sounds);
        }

        public static RequestResultWithSound CreateError(string header, string description = null, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(header, description).MakeCollection(), sounds);
        }

        public static RequestResultWithSound CreateError(SimpleMessage message, SoundSequence sounds = null)
        {
            return CreateError(message.MakeCollection(), sounds);
        }

        public static RequestResultWithSound CreateValidation(string header, string description, ValidationMessageSeverity severity, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity), sounds);
        }

        public static RequestResultWithSound CreateSuccess(string header = null, string description = null, SoundSequence sounds = null)
        {
            return new RequestResultWithSound
            {
                Messages = SimpleMessage.Simple(string.IsNullOrWhiteSpace(header) ? "Success!" : header, description).MakeCollection(),
                Success = true,
                Sounds = sounds ?? SoundsCollection.Success
            };
        }
    }

    public class RequestResultWithSound<T> : RequestResultWithSound
    {

        public T Data { get; private set; }

        private RequestResultWithSound() { }

        public static RequestResultWithSound<T> CreateSuccess(T data, IEnumerable<SimpleMessage> messages, SoundSequence sounds = null)
        {
            return new RequestResultWithSound<T>
            {
                Data = data,
                Messages = messages,
                Success = true,
                Sounds = sounds ?? SoundsCollection.Success
            };
        }

        public static RequestResultWithSound<T> CreateSuccess(T data, ValidationMessages messages, SoundSequence sounds = null)
        {
            return CreateSuccess(data, messages.Select(x => x.Message), sounds);
        }

        public static RequestResultWithSound<T> CreateSuccess(T data, SoundSequence sounds)
        {
            return CreateSuccess(data, (string)null, sounds);
        }

        public static RequestResultWithSound<T> CreateSuccess(T data, string message = null, SoundSequence sounds = null)
        {
            return CreateSuccess(data, SimpleMessage.Simple("Success!", message).MakeCollection(), sounds);
        }

        public static RequestResultWithSound<T> CreateError(IEnumerable<SimpleMessage> messages, T data = default, SoundSequence sounds = null)
        {
            return new RequestResultWithSound<T>
            {
                Messages = messages,
                Data = data,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResultWithSound<T> CreateError(IEnumerable<string> messages, T data = default, SoundSequence sounds = null)
        {
            return CreateError(messages.Select(message => SimpleMessage.Simple(message, null)), data, sounds);
        }

        public static RequestResultWithSound<T> CreateError(string message, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(message, null), data, sounds);
        }

        public static RequestResultWithSound<T> CreateError(string header, string description, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Simple(header, description), data, sounds);
        }

        public static RequestResultWithSound<T> CreateValidation(string header, string description, ValidationMessageSeverity severity, T data = default, SoundSequence sounds = null)
        {
            return CreateError(SimpleMessage.Validation(header, description, severity), data, sounds);
        }

        public static RequestResultWithSound<T> CreateValidation(ValidationMessages messages, T data = default, SoundSequence sounds = null)
        {
            return CreateError(messages.GetWebMessages(), data, sounds);
        }

        public static RequestResultWithSound<T> CreateError(SimpleMessage message, T data = default, SoundSequence sounds = null)
        {
            return CreateError(message.MakeCollection(), data, sounds);
        }

        public static RequestResultWithSound<T> CreateErrorFromOperationResult(IOperationResult operationResult, SoundSequence sounds = null)
        {
            return new RequestResultWithSound<T>
            {
                Data = default,
                Success = false,
                Messages = operationResult.Messages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResultWithSound<T> CreateErrorFromOperationResult(IOperationResult operationResult, string headerOverride, SoundSequence sounds = null)
        {
            var newMessages = operationResult.Messages.Select(x => SimpleMessage.CopyAndOverrideHeader(x, headerOverride));
            return new RequestResultWithSound<T>
            {
                Data = default,
                Success = false,
                Messages = newMessages,
                Sounds = sounds ?? SoundsCollection.Error
            };
        }

        public static RequestResultWithSound<T> CreateErrorFromOperationResults(params IOperationResult[] results)
        {
            return new RequestResultWithSound<T>
            {
                Messages = results.Where(x => !x.Success).SelectMany(x => x.Messages),
                Data = default,
                Success = false,
                Sounds = SoundsCollection.Error
            };
        }

        public static RequestResultWithSound<T> CreateFromOperationResult(IOperationResult<T> operationResult, SoundSequence sounds = null)
        {
            return new RequestResultWithSound<T>
            {
                Data = operationResult.Data,
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }

        public static RequestResultWithSound<T> CreateFromOperationResult<TOp>(IOperationResult<TOp> operationResult, Func<TOp, T> mappingFunc, SoundSequence sounds = null)
        {
            return new RequestResultWithSound<T>
            {
                Data = mappingFunc(operationResult.Data),
                Success = operationResult.Success,
                Messages = operationResult.Messages,
                Sounds = sounds ?? (operationResult.Success ? SoundsCollection.Success : operationResult.HasOnlyWarnings ? SoundsCollection.Warning : SoundsCollection.Error)
            };
        }
    }
}
