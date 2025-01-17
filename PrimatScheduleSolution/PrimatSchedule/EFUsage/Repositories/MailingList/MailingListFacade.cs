﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class MailingListFacade : IDisposable
    {
        private readonly MailingListRepository _repository;

        public MailingListFacade() => _repository = new MailingListRepository();

        private MailingList GetByChatId(string chatId) => GetAll().First(list => list.ChatId == chatId);

        private void Insert(string chatId, TimeSpan time)
        {
            var list = new MailingList()
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                Time = time
            };

            _repository.Insert(list);
        }

        private void Update(MailingList list, TimeSpan time)
        {
            list.Time = time;

            _repository.Update(list);
        }

        public List<MailingList> GetAll() => _repository.GetAll();

        public void InsertOrUpdate(string chatId, TimeSpan time)
        {
            try
            {
                MailingList list = GetByChatId(chatId);

                Update(list, time);
            }
            catch
            {
                Insert(chatId, time);
            }
        }

        public void Remove(string chatId)
        {
            MailingList list = GetByChatId(chatId);

            _repository.Remove(list);
        }

        public void Dispose() => _repository.Dispose();
    }
}
