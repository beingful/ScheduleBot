using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public class Date : IPeriodicity
    {
        public Date() => Name = Buttons.Date;

        public string Name { get; }

        DateTime IPeriodicity.CalculateDate(string message)
        {
            Validation.DateIsValid(message);

            var date = DateTime.Parse(message);

            Validation.DateIsCorrect(date);

            return date;
        }

        string IPeriodicity.GetProperty(DateTime date) => $"{Name}: {date.ToString("dd.MM.yyyy")}\n";

        Guid IPeriodicity.GetPeriodicity()
        {
            using var facade = new PeriodicityFacade();

            return facade.GetDailyPeriodicityId();
        }
    }
}