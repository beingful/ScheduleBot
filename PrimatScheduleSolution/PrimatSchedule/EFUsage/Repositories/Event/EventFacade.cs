using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class EventFacade : IDisposable
    {
        private readonly EventRepository _repository;

        public EventFacade() => _repository = new EventRepository();

        private IEnumerable<Event> GetAll() => _repository.GetAll();

        private IEnumerable<Event> GetAllWhere(Func<Event, bool> isCorrect) => GetAll().Where(@event => isCorrect(@event));

        public bool DoesAnyEventExist(Func<Event, bool> isCorrect) => GetAll().Any(@event => isCorrect(@event));

        public void Insert(Event @event) => _repository.Insert(@event);

        public void Update(Event @event) => _repository.Update(@event);

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

        public IEnumerable<Event> GetByDate(string chatId, DateTime date) 
            => GetAllWhere(@event => @event.ChatId == chatId && @event.Date == date);

        public void Dispose() => _repository.Dispose();
    }
}