using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Get : ICommand
    {
        private readonly DateTime _date;

        public Get(DateTime date) => _date = date;

        public UI Execute(ChatInfo info)
        {
            CheckCommandIsRight(info.LastMessage);

            List<Event> schedule = GetEvent(info.ChatId);

            var message = GetMessage(schedule);

            return new UI(message, Stickers.Walking);
        }

        private string GetMessage(List<Event> schedule)
        {
            var parser = new ScheduleToMessage(schedule);

            return parser.Convert();
        }

        private void CheckCommandIsRight(string message) => Validation.Equal(message, Buttons.Get);

        private List<Event> GetEvent(string chatId)
        {
            var schedule = new Schedule(chatId, _date);

            return schedule.Get();
        }
    }
}