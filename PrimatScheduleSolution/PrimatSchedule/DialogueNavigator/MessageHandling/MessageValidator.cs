using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public static class MessageValidator
    {
        private static DateTime _date;
        private readonly static List<string> _days;

        static MessageValidator()
        {
            _date = default(DateTime);
            _days = new List<string> { "Понеділок", "Вівторок", "Середа", "Четвер", "П'ятниця", "Субота", "Неділя" };
        }

        public static DateTime TryGetDateTime(string data)
        {
            DateTime date = default(DateTime);
            DateTime.TryParse(data, out date);

            if (date >= DateTime.Now)
            {
                return date;
            }

            throw new IncorrectMessageException();
        }
    }
}
