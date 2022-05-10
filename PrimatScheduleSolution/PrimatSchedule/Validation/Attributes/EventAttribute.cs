using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    class EventAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is Event @event)
            {
                Validation.RequiredIsNotDefault(@event.Name);

                Validation.RequiredIsNotDefault(@event.Date);

                Validation.DateIsCorrect(@event.Date);

                Validation.StartAndEndAreNormal(@event.StartTime, @event.EndTime);

                Validation.EndAfterStart(@event.StartTime, @event.EndTime);

                Validation.ValidateTimeForDuplications(@event);
            }

            return false;
        }
    }
}
