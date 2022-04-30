using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Date : ICalendarDay, IPeriodicity
    {
        public Date() => Name = Buttons.Date;

        public string Name { get; }

        public DateTime TryGetDate(string message)
        {
            MessageValidator.ValidateDate(message);

            var date = DateTime.Parse(message);

            MessageValidator.ValidateIsDateCorrect(date);

            return date;
        }

        string IPeriodicity.GetProperty(DateTime date) => $"{Name}: {date}";

        Guid IPeriodicity.GetPeriodicity()
        {
            using var facade = new PeriodicityFacade();

            return facade.GetDailyPeriodicityId();
        }
    }
}