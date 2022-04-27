using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    class EventValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Event @event)
            {
                MessageValidator.ValidateIsEndWithoutStart(@event.StartTime, @event.EndTime);

                MessageValidator.ValidateIsEndWithoutStart(@event.StartTime, @event.EndTime);

                MessageValidator.ValidateRequiredIsDefault(@event.Name, default(string));

                MessageValidator.ValidateRequiredIsDefault(@event.Date, default(DateTime));

                MessageValidator.ValidateIsDateCorrect(@event.Date);
            }

            return false;
        }
    }
}
