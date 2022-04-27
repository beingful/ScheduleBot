using System;

namespace PrimatScheduleBot
{
    public interface IPeriodicity
    {
        public string Name { get; }
        public string GetProperty(DateTime date);
        public Guid GetPeriodicity();
    }
}
