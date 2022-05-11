using System;

namespace PrimatScheduleBot
{
    public static class DateTimeExtension
    {
        public static DateTime NextDateByDay(this DateTime date, DayOfWeek day)
        {
            int diff = ((int)day - (int)date.DayOfWeek + 6) % 7;

            return date.AddDays(diff + 1);
        }
    }
}
