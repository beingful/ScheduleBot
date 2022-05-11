using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class EventRepository : IDisposable
    {
        private readonly TimetableContext _context;

        public EventRepository() => _context = new TimetableContext();

        public List<Event> GetAll() => _context.Events.ToList();

        public async void Insert(Event @event)
        {
            await _context.AddAsync(@event);

            SaveChanges();
        }

        public void Update(Event @event)
        {
            _context.Update(@event);

            SaveChanges();
        }

        public void Remove(Event @event)
        {
            _context.Remove(@event);

            SaveChanges();
        }

        public void RemoveRange(IEnumerable<Event> events)
        {
            _context.RemoveRange(events);

            SaveChanges();
        }

        private void SaveChanges() => _context.SaveChanges();

        public async void Dispose() => await _context.DisposeAsync();
    }
}
