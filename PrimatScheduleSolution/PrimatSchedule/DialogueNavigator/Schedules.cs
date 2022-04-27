﻿using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class Schedules
    {
        private readonly string _token;

        public Schedules(string token) => _token = token;

        public void Start()
        {
            StartDefault();
            StartExisted();
        }

        public void StartExisted()
        {
            using var mailingListFacade = new MailingListFacade();

            IEnumerable<MailingList> lists = mailingListFacade.GetAll();

            foreach (var list in lists)
            {
                var mailing = new Mailing(list.ChatId);

                mailing.Start(list.Time, _token);
            }
        }

        private void StartDefault()
        {
            var schedule = new Scheduler(_token);

            schedule.Start<DataActualisator>(TimeSpan.Zero);
        }
    }
}