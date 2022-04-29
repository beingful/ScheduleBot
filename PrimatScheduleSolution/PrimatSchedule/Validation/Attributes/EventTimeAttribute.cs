using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class EventTimeAttribute : ValidationAttribute
    {
        private bool _result;

        public override bool IsValid(object value)
        {
            if (value is Event @event)
            {
                using var facade = new EventFacade();

                _result = !facade.DoesAnyEventExist(otherEvent => DoesIntersectionsExist(@event, otherEvent));
            }

            return _result;
        }

        private bool DoesIntersectionsExist(Event @event, Event otherEvent)
        {
            return @event.Id != otherEvent.Id
                && @event.ChatId == otherEvent.ChatId
                && @event.Date == otherEvent.Date
                && ((otherEvent.StartTime >= @event.StartTime && otherEvent.StartTime < @event.EndTime)
                    || (otherEvent.EndTime > @event.StartTime && otherEvent.EndTime <= @event.EndTime)
                    || (@event.StartTime >= otherEvent.StartTime && @event.StartTime < otherEvent.EndTime)
                    || (@event.EndTime > otherEvent.StartTime && @event.EndTime <= otherEvent.EndTime));
        }
    }
}