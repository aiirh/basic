using Aiirh.Basic.Utilities;

namespace Aiirh.Basic.Entities
{
    public static class FilterPropertyExtensions
    {
        public static bool IsFilterRequired<T>(this FilterProperty<T> property)
        {
            if (property == null)
            {
                return false;
            }

            if (property.EmptyFilterValueBehavior == EmptyFilterValueBehavior.Filter)
            {
                return true;
            }

            if (property.Value == null)
            {
                return false;
            }

            return true;
        }
    }
}
