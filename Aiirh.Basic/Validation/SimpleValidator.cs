using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiirh.Basic.Validation;

public abstract class SimpleValidator<TEntity>
{
    public virtual async Task<IEnumerable<ValidationResult<TEntity>>> ValidateMany(IEnumerable<TEntity> entities)
    {
        var results = new List<ValidationResult<TEntity>>();

        foreach (var entity in entities)
        {
            var validationMessages = await Validate(entity);
            if (!validationMessages.IsValid)
            {
                results.Add(new ValidationResult<TEntity>
                {
                    InvalidEntity = entity,
                    Messages = validationMessages
                });
            }
        }

        return results;
    }

    public abstract Task<ValidationMessages> Validate(TEntity entity);
}