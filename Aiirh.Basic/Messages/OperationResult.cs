using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Exceptions;
using Aiirh.Basic.Utilities;
using Aiirh.Basic.Validation;

namespace Aiirh.Basic.Messages;

public interface IOperationResult
{
    bool Success { get; }

    bool HasOnlyWarnings { get; }

    IEnumerable<SimpleMessage> Messages { get; }
}

public interface IOperationResult<out T> : IOperationResult
{
    T Data { get; }
}

public class OperationResult : IOperationResult
{
    public bool Success { get; set; }

    public bool HasOnlyWarnings => Messages.Any() && Messages.All(x => x.IsWarning);

    public IEnumerable<SimpleMessage> Messages { get; set; }

    public static IOperationResult CreateSuccess()
    {
            return new OperationResult
            {
                Success = true,
                Messages = Enumerable.Empty<SimpleMessage>()
            };
        }

    public static IOperationResult CreateSuccess(IEnumerable<SimpleMessage> messages)
    {
            return new OperationResult
            {
                Success = true,
                Messages = messages
            };
        }

    public static IOperationResult CreateError(Exception e)
    {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Error(e.Message, e.LogInnerExceptions()).MakeCollection();
            return new OperationResult
            {
                Messages = messages
            };
        }

    public static IOperationResult CreateError(IEnumerable<string> messages)
    {
            return new OperationResult
            {
                Messages = messages.Select(x => SimpleMessage.Error(x, null))
            };
        }

    public static IOperationResult CreateError(IEnumerable<SimpleMessage> messages)
    {
            return new OperationResult
            {
                Messages = messages,
            };
        }

    public static IOperationResult CreateError(string message)
    {
            return new OperationResult
            {
                Messages = SimpleMessage.Error(message, null).MakeCollection()
            };
        }

    public static IOperationResult CreateError(string header, string description)
    {
            return new OperationResult
            {
                Messages = SimpleMessage.Error(header, description).MakeCollection()
            };
        }

    public static IOperationResult CreateValidationErrors(string header, string description, ValidationMessageSeverity severity)
    {
            return new OperationResult
            {
                Messages = SimpleMessage.Validation(header, description, severity).MakeCollection()
            };
        }

    public static IOperationResult CreateValidationErrors(ValidationMessages messages)
    {
            return new OperationResult
            {
                Messages = messages.Select(x => x.Message)
            };
        }

    public static IOperationResult CreateErrorFromOperationResult(IOperationResult result)
    {
            var operationResult = result as OperationResult;
            return new OperationResult
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Success = false
            };
        }
}

public class OperationResult<T> : OperationResult, IOperationResult<T>
{
    public T Data { get; set; }

    public static IOperationResult<T> CreateFromOperationResult(IOperationResult result, T data)
    {
            var operationResult = result as OperationResult;
            return new OperationResult<T>
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Data = data,
                Success = result.Success
            };
        }

    public new static IOperationResult<T> CreateErrorFromOperationResult(IOperationResult result)
    {
            var operationResult = result as OperationResult;
            return new OperationResult<T>
            {
                Messages = operationResult?.Messages ?? Enumerable.Empty<SimpleMessage>(),
                Data = default,
                Success = false
            };
        }

    public new static IOperationResult<T> CreateError(IEnumerable<SimpleMessage> messages)
    {
            return new OperationResult<T>
            {
                Messages = messages,
                Data = default
            };
        }

    public static IOperationResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
    {
            return new OperationResult<T>
            {
                Messages = results.Where(x => !x.Success).SelectMany(x => x.Messages),
                Data = default,
                Success = false
            };
        }

    public static IOperationResult<T> CreateError(Exception e, T data = default)
    {
            var messages = e is SimpleException se ? se.Messages : SimpleMessage.Error(e.Message, e.LogInnerExceptions()).MakeCollection();
            return new OperationResult<T>
            {
                Messages = messages,
                Data = data
            };
        }

    public static IOperationResult<T> CreateError(Exception e, string header)
    {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(header, e.Message).MakeCollection(),
                Data = default
            };
        }

    public static IOperationResult<T> CreateError(IEnumerable<string> messages, T data = default)
    {
            return new OperationResult<T>
            {
                Messages = messages.Select(x => SimpleMessage.Error(x, null)),
                Data = data
            };
        }

    public static IOperationResult<T> CreateError(string message, T data = default)
    {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(message, null).MakeCollection(),
                Data = data
            };
        }

    public static IOperationResult<T> CreateError(string header, string message, T data = default)
    {
            return new OperationResult<T>
            {
                Messages = SimpleMessage.Error(header, message).MakeCollection(),
                Data = data
            };
        }

    public static IOperationResult<T> CreateSuccess(T data, string message = null)
    {
            return new OperationResult<T>
            {
                Data = data,
                Messages = SimpleMessage.Simple("Success!", message).MakeCollection(),
                Success = true
            };
        }

    public static IOperationResult<T> CreateSuccess(T data, IEnumerable<SimpleMessage> messages)
    {
            return new OperationResult<T>
            {
                Data = data,
                Messages = messages,
                Success = true
            };
        }

    public static IOperationResult<T> CreateValidationErrors(ValidationMessages messages, T data = default)
    {
            return new OperationResult<T>
            {
                Data = data,
                Messages = messages.Select(x => x.Message)
            };
        }

    public static IOperationResult<T> CreateValidationErrors(string header, string description, ValidationMessageSeverity severity, T data = default)
    {
            return new OperationResult<T>
            {
                Data = data,
                Messages = SimpleMessage.Validation(header, description, severity).MakeCollection()
            };
        }
}