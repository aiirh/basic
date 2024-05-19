using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Messages;

namespace Aiirh.Basic.Validation;

public class ValidationMessages : IEnumerable<ValidationMessage>
{
    private readonly List<ValidationMessage> _messages = [];

    public bool IsValid => !_messages.Any();

    public bool IsValidOrOnlyWarnings => IsValid || _messages.All(x => x.Severity == ValidationMessageSeverity.Warning);

    public ValidationMessage this[int index]
    {
        get => _messages[index];
        set => _messages.Insert(index, value);
    }

    public IEnumerator<ValidationMessage> GetEnumerator()
    {
        return _messages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void AddIfInvalid(ValidationCheck check)
    {
        if (check.IsValid)
        {
            return;
        }
        _messages.Add(new ValidationMessage(check));
    }

    public void Add(string header, string description, ValidationMessageSeverity severity)
    {
        _messages.Add(new ValidationMessage(header, description, severity));
    }

    public void Add(string message, ValidationMessageSeverity severity)
    {
        _messages.Add(new ValidationMessage(message, null, severity));
    }

    public void AddRange(ValidationMessages messages)
    {
        _messages.AddRange(messages);
    }

    public void AddRange(IEnumerable<ValidationMessage> messages)
    {
        if (messages == null)
        {
            return;
        }
        _messages.AddRange(messages);
    }

    public string GetMessagesAsString()
    {
        return string.Join("; ", _messages.Select(x => x.ToString()));
    }

    public string GetApiMessagesWithSeparatedWarnings(bool printType)
    {
        var errors = string.Join("|", _messages.Where(x => x.Severity == ValidationMessageSeverity.Error).Select(x => $"{(printType ? "ERROR: " : string.Empty)}{x.Message}"));
        var warnings = string.Join("|", _messages.Where(x => x.Severity == ValidationMessageSeverity.Warning).Select(x => $"{(printType ? "WARNING: " : string.Empty)}{x.Message}"));
        return string.IsNullOrWhiteSpace(errors)
            ? warnings
            : string.IsNullOrWhiteSpace(warnings)
                ? errors
                : string.Join("|", errors, warnings);
    }

    public IEnumerable<SimpleMessage> GetMessages()
    {
        return _messages.Select(x => x.Message);
    }

    public ValidationMessages()
    {
    }

    public ValidationMessages(IEnumerable<ValidationMessage> messages)
    {
        _messages = messages.ToList();
    }
}