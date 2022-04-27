using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class EventRepository : IDisposable
    {
        private readonly TimetableContext _context;

        public EventRepository() => _context = new TimetableContext();

        public IEnumerable<Event> GetAll() => _context.Events;

        public async void Insert(Event @event)
        {
            await _context.AddAsync(@event);

            SaveChangesAsync();
        }

        public void Update(Event @event)
        {
            _context.Update(@event);

            SaveChangesAsync();
        }

        public void Remove(Event @event)
        {
            _context.Remove(@event);

            SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<Event> events)
        {
            _context.RemoveRange(events);

            SaveChangesAsync();
        }

        private async void SaveChangesAsync() => await _context.SaveChangesAsync();

        public async void Dispose() => await _context.DisposeAsync();
    }
}
