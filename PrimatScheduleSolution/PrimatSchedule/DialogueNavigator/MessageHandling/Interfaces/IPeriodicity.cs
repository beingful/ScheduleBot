using System;

namespace PrimatScheduleBot
{
    public interface IPeriodicity
    {
        public string Name { get; }
        public string GetProperty(DateTime date);
        public DateTime TryGetDate(string message);
        public Guid GetPeriodicity();
    }
}
