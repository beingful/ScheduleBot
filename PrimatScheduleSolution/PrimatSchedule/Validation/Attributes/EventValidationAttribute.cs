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
                MessageValidator.ValidateRequiredIsDefault(@event.Name, null);

                MessageValidator.ValidateRequiredIsDefault(@event.Date, DateTime.MinValue);

                MessageValidator.ValidateIsDateCorrect(@event.Date);

                MessageValidator.ValidateIsEndWithoutStart(@event.StartTime, @event.EndTime);

                MessageValidator.ValidateIsEndBeforeStart(@event.StartTime, @event.EndTime);

                MessageValidator.ValidateTimeForDuplications(@event);
            }

            return false;
        }
    }
}
