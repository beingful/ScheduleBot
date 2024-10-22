﻿using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class ScheduleGetter
    {
        private readonly string _chatId;
        private readonly DateTime _date;

        public ScheduleGetter(string chatId, DateTime date) 
        {
            _chatId = chatId;
            _date = date;
        }

        public List<Event> Get()
        {
            using var facade = new EventFacade();

            List<Event> schedule = facade.GetByDate(_chatId, _date);

            Validation.ScheduleNotEmpty(schedule.Count);

            return schedule;
        }
    }
}