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
                if (string.IsNullOrEmpty(@event.Date) && string.IsNullOrEmpty(@event.Day))
                {
                    throw new NoDateAndDayException();
                }

                if (!string.IsNullOrEmpty(@event.EndTime))
                {
                    if (string.IsNullOrEmpty(@event.StartTime))
                    {
                        throw new EndWithoutStartException();
                    }
                    else if (TimeSpan.Compare(TimeSpan.Parse(@event.StartTime), TimeSpan.Parse(@event.EndTime)) is 1)
                    {
                        throw new StartAfterEndException();
                    }
                }

                return true;
            }

            return false;
        }
    }
}
