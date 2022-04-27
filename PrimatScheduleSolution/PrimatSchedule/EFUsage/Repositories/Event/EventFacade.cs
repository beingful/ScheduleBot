using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class EventFacade : IDisposable
    {
        private readonly EventRepository _repository;

        public EventFacade() => _repository = new EventRepository();

        private IEnumerable<Event> GetAllWhere(Func<Event, bool> isCorrect) 
            => _repository.GetAll().Where(@event => isCorrect(@event));

        private void ValidateEvent(Event @event)
        {
            var timeValidator = new EventTimeValidation(@event, _repository);

            Validator.CheckDateTimeDuplications(timeValidator.IsValid());
        }

        public void Insert(Event @event) 
        {
            ValidateEvent(@event);

            _repository.Insert(@event);
        }

        public void Update(Event @event) 
        {
            ValidateEvent(@event);

            _repository.Update(@event);
        }

        public void RemoveAllBeforeToday() 
        {
            IEnumerable<Event> events = GetAllWhere(@event => @event.Date < DateTime.Today);

            _repository.RemoveRange(events);
        }

        public void Remove(Guid id)
        {
            Event @event = _repository.GetAll().First(@event => @event.Id == id);

            _repository.Remove(@event);
        }

        public IEnumerable<Event> GetByDate(DateTime date) => GetAllWhere(@event => @event.Date == date);

        public void Dispose() => _repository.Dispose();
    }
}