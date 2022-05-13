using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class EventFacade : IDisposable
    {
        private readonly EventRepository _repository;

        public EventFacade() => _repository = new EventRepository();

        private List<Event> GetAll() => _repository.GetAll();

        private List<Event> GetAllWhere(Func<Event, bool> isCorrect) => GetAll().Where(@event => isCorrect(@event)).ToList();

        public Event GetById(Guid id) => _repository.GetAll().First(@event => @event.Id == id);

        public bool DoesAnyEventExist(Func<Event, bool> isCorrect) => GetAll().Any(@event => isCorrect(@event));

        public void Insert(Event @event) => _repository.Insert(@event);

        public void Update(Event @event) => _repository.Update(@event);

        public void RemoveAllBeforeToday() 
        {
            using var facade = new PeriodicityFacade();

            List<Event> events = GetAllWhere(@event => @event.Date < DateTime.Today 
                && @event.PeriodicityId == facade.GetDailyPeriodicityId());

            _repository.RemoveRange(events);
        }   

        public void Remove(Event @event) => _repository.Remove(@event);

        public void AddWeek()
        {
            using var facade = new PeriodicityFacade();

            List<Event> events = GetAllWhere(@event => @event.Date < DateTime.Today 
                && @event.PeriodicityId == facade.GetWeeklyPeriodicityId());

            foreach (var @event in events)
            {
                @event.Date = @event.Date.AddDays(7);

                Update(@event);
            }
        }

        public List<Event> GetByDate(string chatId, DateTime date) 
            => GetAllWhere(@event => @event.ChatId == chatId && @event.Date == date);

        public void Dispose() => _repository.Dispose();
    }
}