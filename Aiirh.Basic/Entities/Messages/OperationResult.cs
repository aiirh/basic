using System;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Validation;
using Aiirh.Basic.Extensions;
using Newtonsoft.Json;

namespace Aiirh.Basic.Entities.Messages
{
	public interface IOperationResult
	{
		bool Success { get; }
		bool HasOnlyWarnings { get; }

		IEnumerable<WebMessage> WebMessages { get; }
		[JsonIgnore]
		IEnumerable<string> ApiMessages { get; }
		[JsonIgnore]
		string SimpleMessage { get; }
		[Obsolete("Use explicitly typed OperationResult<T>")]
		T GetData<T>();
	}

	public interface IOperationResult<out T> : IOperationResult
	{
		T Data { get; }
	}

	public class OperationResult : IOperationResult
	{

		public bool Success { get; set; }
		public bool HasOnlyWarnings => WebMessages.Any() && WebMessages.All(x => x.IsValidationWarning);
		public IEnumerable<WebMessage> WebMessages { get; set; }

		[JsonIgnore]
		public IEnumerable<string> ApiMessages => WebMessages.Select(x => x.ApiMessage);
		[JsonIgnore]
		public string SimpleMessage => string.Join("; ", ApiMessages);

		public static IOperationResult CreateSuccess()
		{
			return new OperationResult
			{
				Success = true,
				WebMessages = Enumerable.Empty<WebMessage>()
			};
		}

		public static IOperationResult CreateSuccess(IEnumerable<WebMessage> messages)
		{
			return new OperationResult
			{
				Success = true,
				WebMessages = messages
			};
		}

		public static IOperationResult CreateError(Exception e)
		{
			return new OperationResult
			{
				WebMessages = WebMessage.Simple(e.Message, e.LogException()).MakeCollection(),
			};
		}

		public static IOperationResult CreateError(IEnumerable<string> messages)
		{
			return new OperationResult
			{
				WebMessages = messages.Select(x => WebMessage.Simple(x, null, x))
			};
		}

		public static IOperationResult CreateError(string message)
		{
			return new OperationResult
			{
				WebMessages = WebMessage.Simple(message, null, message).MakeCollection()
			};
		}

		public static IOperationResult CreateError(string header, string description)
		{
			return new OperationResult
			{
				WebMessages = WebMessage.Simple(header, description, $"{header}. {description}").MakeCollection()
			};
		}

		public static IOperationResult CreateValidationErrors(string header, string description, ValidationMessageSeverity severity)
		{
			return new OperationResult
			{
				WebMessages = WebMessage.Validation(header, description, $"{header}. {description}", severity).MakeCollection()
			};
		}

		[Obsolete("Use explicitly typed OperationResult<T>")]
		public T GetData<T>()
		{
			if (this is IOperationResult<T> concreteOperationResult)
			{
				return concreteOperationResult.Data;
			}
			if (this is IOperationResult<IErrorMessage> operationResultWithMessage && typeof(IErrorMessage).IsAssignableFrom(typeof(T)))
			{
				return (T)operationResultWithMessage.Data;
			}
			return default;
		}

		public static IOperationResult CreateValidationErrors(ValidationMessages messages)
		{
			return new OperationResult
			{
				WebMessages = messages.Select(x => x.WebMessage)
			};
		}

		public static OperationResult CreateErrorFromOperationResult(IOperationResult result)
		{
			var operationResult = result as OperationResult;
			return new OperationResult
			{
				WebMessages = operationResult?.WebMessages ?? Enumerable.Empty<WebMessage>(),
				Success = false
			};
		}
	}

	public class OperationResult<T> : OperationResult, IOperationResult<T>
	{
		public T Data { get; set; }

		public static OperationResult<T> CreateFromOperationResult(IOperationResult result, T data)
		{
			var operationResult = result as OperationResult;
			return new OperationResult<T>
			{
				WebMessages = operationResult?.WebMessages ?? Enumerable.Empty<WebMessage>(),
				Data = data,
				Success = result.Success
			};
		}

		public new static OperationResult<T> CreateErrorFromOperationResult(IOperationResult result)
		{
			var operationResult = result as OperationResult;
			return new OperationResult<T>
			{
				WebMessages = operationResult?.WebMessages ?? Enumerable.Empty<WebMessage>(),
				Data = default,
				Success = false
			};
		}

		public static OperationResult<T> CreateErrorFromOperationResults(params IOperationResult[] results)
		{
			return new OperationResult<T>
			{
				WebMessages = results.Where(x => !x.Success).SelectMany(x => x.WebMessages),
				Data = default,
				Success = false
			};
		}

		public static OperationResult<T> CreateError(Exception e, T data = default)
		{
			return new OperationResult<T>
			{
				WebMessages = WebMessage.Simple(e.Message, e.LogException()).MakeCollection(),
				Data = data
			};
		}

		public static OperationResult<T> CreateError(Exception e, string header)
		{
			return new OperationResult<T>
			{
				WebMessages = WebMessage.Simple(header, e.Message).MakeCollection(),
				Data = default
			};
		}

		public static OperationResult<T> CreateError(IEnumerable<string> messages, T data = default)
		{
			return new OperationResult<T>
			{
				WebMessages = messages.Select(x => WebMessage.Simple(x, null, x)),
				Data = data
			};
		}

		public static OperationResult<T> CreateError(string message, T data = default)
		{
			return new OperationResult<T>
			{
				WebMessages = WebMessage.Simple(message, null, message).MakeCollection(),
				Data = data
			};
		}

		public static OperationResult<T> CreateError(string header, string message, T data = default)
		{
			return new OperationResult<T>
			{
				WebMessages = WebMessage.Simple(header, message, $"{header}. {message}").MakeCollection(),
				Data = data
			};
		}

		public static OperationResult<T> CreateSuccess(T data, string message = null)
		{
			return new OperationResult<T>
			{
				Data = data,
				WebMessages = WebMessage.Simple("Success!", message, message).MakeCollection(),
				Success = true
			};
		}

		public static OperationResult<T> CreateValidationErrors(ValidationMessages messages, T data = default)
		{
			return new OperationResult<T>
			{
				Data = data,
				WebMessages = messages.Select(x => x.WebMessage)
			};
		}

		public static OperationResult<T> CreateValidationErrors(string header, string description, ValidationMessageSeverity severity, T data = default)
		{
			return new OperationResult<T>
			{
				Data = data,
				WebMessages = WebMessage.Validation(header, description, $"{header}. {description}", severity).MakeCollection()
			};
		}
	}
}
