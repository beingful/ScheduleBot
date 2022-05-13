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

        //private static bool AreEqual(string day, string anotherDay)
        //{
        //    int index = Array.IndexOf(_days, day);

        //    //Console.WriteLine(FormateDay(day, index));
        //    //Console.WriteLine(FormateDay(anotherDay, index));

        //    return FormateDay(day, index) == FormateDay(anotherDay, index);
        //}

        private static void FormatFriday(ref string day) => day = day.Remove(1, 1).Insert(1, $"{(char)39}");

        //private static string FormateDay(string day, int index)
        //{
        //    day = day.ToLower();

        //    if (index is 5)
        //    {
        //        day = RemoveSymbols(day);
        //    }

        //    return day;
        //}

        //private static string RemoveSymbols(string day) 
        //    => String.Join(String.Empty, day.Where(letter => (int)letter != 700 && (int)letter != 39));

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
