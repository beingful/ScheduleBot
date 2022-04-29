using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Day : ICalendarDay, IPeriodicity
    {
        public Day() => Name = Buttons.Day;

        public string Name { get; }

        private DayOfWeek GetDayOfWeek(string message) => DaysOfWeek.GetDayOfWeekByUKR(message);

        private string DayFromDate(DateTime date)
        {
            int dayNumber = date.Day;

            return DaysOfWeek.GetDay(dayNumber);
        }

        private DateTime GetDateByDay(string message)
        {
            DayOfWeek day = GetDayOfWeek(message);

            return DateTime.Today.NextDateByDay(day);
        }

        public DateTime TryGetDate(string message)
        {
            MessageValidator.ValidateDay(message);

            return GetDateByDay(message);
        }

        string IPeriodicity.GetProperty(DateTime date) => $"{Name}: {DayFromDate(date)}";

        Guid IPeriodicity.GetPeriodicity()
        {
            using var facade = new PeriodicityFacade();

            return facade.GetWeeklyPeriodicityId();
        }
    }
}