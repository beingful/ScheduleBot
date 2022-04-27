using System;
using System.Globalization;
using System.Linq;

namespace PrimatScheduleBot
{
    public static class DaysOfWeek
    {
        private static readonly string[] _days;

        static DaysOfWeek() => _days = CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat.DayNames;

        public static bool DoesSuchADayExist(string day) => _days.Any(dayOfWeek => dayOfWeek == day);

        public static DayOfWeek GetDayOfWeekByUKR(string day)
        {
            int dayIndex = Array.IndexOf(_days, day);

            return (DayOfWeek)Enum.GetValues(typeof(DayOfWeek)).GetValue(dayIndex);
        }

        public static string GetDay(int index) => _days[index];
    }
}
