using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class MailingListRepository : IDisposable
    {
        private readonly TimetableContext _context;

        public MailingListRepository() => _context = new TimetableContext();

        public IEnumerable<MailingList> GetAll() => _context.MailingLists;

        public async void Insert(MailingList list)
        {
            await _context.AddAsync(list);

            SaveChangesAsync();
        }

        public void Update(MailingList list)
        {
            _context.Update(list);

            SaveChangesAsync();
        }

        public void Remove(MailingList list)
        {
            _context.Remove(list);

            SaveChangesAsync();
        }

        private async void SaveChangesAsync() => await _context.SaveChangesAsync();

        public async void Dispose() => await _context.DisposeAsync();
    }
}
