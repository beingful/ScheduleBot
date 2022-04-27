using System;

namespace PrimatScheduleBot
{
    public interface ICalendarDay
    {
        public DateTime TryGetDate(string message);
    }
}
