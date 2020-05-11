using System.Collections.Generic;
using System.Linq;

namespace Aiirh.Basic.Entities.Validation
{
    public class ValidationResult<T>
    {
        public T InvalidEntity { get; set; }
        public IEnumerable<ValidationMessage> Messages { get; set; }

        public string GetSimpleMessage()
        {
            return string.Join(";", Messages?.Select(x => $"{x.WebMessage.Header}: {x.WebMessage.Description}") ?? Enumerable.Empty<string>());
        }
    }
}
