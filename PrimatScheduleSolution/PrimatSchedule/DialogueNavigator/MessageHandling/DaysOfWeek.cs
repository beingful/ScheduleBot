using System;
using System.Globalization;
using System.Linq;

namespace PrimatScheduleBot
{
    public static class DaysOfWeek
    {
        private static readonly string[] _days;

        static DaysOfWeek() 
        {
            _days = CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat.DayNames;
            FormatFriday(ref _days[5]);
        }
        
        private static void FormatFriday(ref string day) => day = day.Remove(1, 1).Insert(1, $"{(char)39}");

        public static bool DoesSuchADayExist(string day) 
            => _days.Any(dayOfWeek => dayOfWeek == day.ToLower());

        public static DayOfWeek GetDayOfWeekByUKR(string day)
        {
            int dayIndex = Array.IndexOf(_days, day.ToLower());

            return (DayOfWeek)Enum.GetValues(typeof(DayOfWeek)).GetValue(dayIndex);
        }

        public static string GetDay(int index) => _days[index];
    }
}
