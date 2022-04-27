using System.Linq;

namespace PrimatScheduleBot
{
    public class EventTimeValidation
    {
        private readonly Event _event;
        private readonly EventRepository _repository;

        public EventTimeValidation(Event @event, EventRepository repository)
        {
            _event = @event;
            _repository = repository;
        }

        public bool IsValid()
        {
            return !_repository
                .GetAll()
                .Any(@event => DoesIntersectionsExist(@event));
        }

        private bool DoesIntersectionsExist(Event @event)
        {
            return _event.Id != @event.Id
                && _event.ChatId == @event.ChatId
                && _event.Date == @event.Date
                && ((@event.StartTime >= _event.StartTime && @event.StartTime < _event.EndTime)
                    || (@event.EndTime > _event.StartTime && @event.EndTime <= _event.EndTime)
                    || (_event.StartTime >= @event.StartTime && _event.StartTime < @event.EndTime)
                    || (_event.EndTime > @event.StartTime && _event.EndTime <= @event.EndTime));
        }
    }
}