using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aiirh.Basic.Validation;

public abstract class Validator<TEntity, TParams> where TParams : IValidationParameters
{
    public virtual async Task<IEnumerable<ValidationResult<TEntity>>> ValidateMany(IEnumerable<TEntity> entities, TParams parameters)
    {
        var results = new List<ValidationResult<TEntity>>();

        foreach (var entity in entities)
        {
            var validationMessages = await Validate(entity, parameters);
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

    public abstract Task<ValidationMessages> Validate(TEntity entity, TParams parameters);
}

public interface IValidationParameters;