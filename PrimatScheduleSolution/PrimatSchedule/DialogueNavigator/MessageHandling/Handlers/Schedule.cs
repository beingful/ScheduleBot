﻿using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class Schedule
    {
        private readonly string _chatId;
        private readonly DateTime _date;

        public Schedule(string chatId, DateTime date) 
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

        //private List<Event> GetSchedule(string chatId, DateTime date)
        //{
        //    using var facade = new EventFacade();

        //    List<Event> schedule = facade.GetByDate(chatId, date);

        //    Validator.ScheduleNotEmpty(schedule.Count);

        //    return schedule;
        //}

        //public UI Execute(ChatInfo info) 
        //{
        //    if (_currentState is null)
        //    {
        //        TryChangeCurrentState(info);
        //    }

        //    return _currentState.Execute(info);
        //}

        //public void TryChangeCurrentState(ChatInfo info)
        //{
        //    List<Event> schedule = GetSchedule(info.ChatId, _date);

        //    _currentState = new Command(new UIBehaviour(new Dictionary<string, UI>
        //    {
        //        { info.LastMessage, new UI("Що робимо далі?", new List<string> { Buttons.Get, Buttons.Event }) }
        //    }), new StateBehaviour(new Dictionary<string, ICommand>
        //    {
        //        { Buttons.Get, new Get(schedule) },
        //        { Buttons.Event, new ChooseEvent(schedule, _period) }
        //    }));
        //}
    }
}