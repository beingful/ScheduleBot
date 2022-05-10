using System;

namespace PrimatScheduleBot
{
    public interface ICalendarDay
    {
        public DateTime CalculateDate(string message);
    }
}
