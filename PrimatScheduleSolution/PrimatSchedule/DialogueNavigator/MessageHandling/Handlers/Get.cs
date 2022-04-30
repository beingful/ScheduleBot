using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Get : ICommand
    {
        private readonly List<Event> _schedule;

        public Get(List<Event> schedule) => _schedule = schedule;

        public UI Execute(ChatInfo info)
        {
            MessageValidator.ValidateMessage(info.LastMessage is Buttons.Get);
            
            var parser = new ScheduleToMessage(_schedule);

            var scheduleInMessage = parser.Convert();

            return new UI(scheduleInMessage, Stickers.Walking);
        }
    }
}