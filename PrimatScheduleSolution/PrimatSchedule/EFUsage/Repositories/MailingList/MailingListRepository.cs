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

            SaveChanges();
        }

        public void Update(MailingList list)
        {
            _context.Update(list);

            SaveChanges();
        }

        public void Remove(MailingList list)
        {
            _context.Remove(list);

            SaveChanges();
        }

        private void SaveChanges() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
