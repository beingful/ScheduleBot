using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class NotNullAttribute : ValidationAttribute
    {
        private readonly string _displayValue;

        public NotNullAttribute(string displayValue)
        {
            _displayValue = displayValue;
        }

        public override bool IsValid(object value)
        {
            if (value is null)
            {
                throw new NullValueException(_displayValue);
            }

            return true;
        }
    }
}
