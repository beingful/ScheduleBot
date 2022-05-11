using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class MailingListRepository : IDisposable
    {
        private readonly TimetableContext _context;

        public MailingListRepository() => _context = new TimetableContext();

        public List<MailingList> GetAll() => _context.MailingLists.ToList();

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

        public async void Dispose() => await _context.DisposeAsync();
    }
}
