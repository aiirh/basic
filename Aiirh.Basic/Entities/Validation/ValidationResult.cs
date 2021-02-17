using System.Collections.Generic;
using System.Linq;
using Aiirh.Basic.Entities.Messages;

namespace Aiirh.Basic.Entities.Validation
{
    public class ValidationResult<T>
    {
        public T InvalidEntity { get; set; }
        public IEnumerable<ValidationMessage> Messages { get; set; }

        public string GetSimpleMessage()
        {
            return string.Join("; ", Messages?.Select(x => MessageBuilder.BuildMessage(x.Message.Header, x.Message.Description)) ?? Enumerable.Empty<string>());
        }
    }
}
