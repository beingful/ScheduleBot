using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class PeriodicityRepository : IDisposable
    {
        private readonly TimetableContext _context;

        public PeriodicityRepository() => _context = new TimetableContext();

        public IEnumerable<Periodicity> GetAll() => _context.Periodicities.EntityType.BaseType.;

        public async void Dispose() => await _context.DisposeAsync();
    }
}
