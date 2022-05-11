using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class IsDefaultAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var type = new TypeDetermination(value.GetType());
            object defaultValue;

            defaultValue = type.GetTypeDefaultValue();

            return !value.EqualsTo(defaultValue);
        }
    }
}
