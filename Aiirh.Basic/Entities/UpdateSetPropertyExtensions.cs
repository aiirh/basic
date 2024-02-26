using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Entities;

public static class UpdateSetPropertyExtensions
{
    public static bool IsUpdateRequired<T>(this UpdateSetProperty<T> property)
    {
            if (property == null)
            {
                return false;
            }

            if (property.UpdateIfNull)
            {
                return true;
            }

            if (property.Value.IsNullOrDefault())
            {
                return false;
            }

            return true;
        }
}