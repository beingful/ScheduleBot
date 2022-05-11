using System;

namespace PrimatScheduleBot
{
    [Serializable]
    public partial class Event
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; }
        public string Name { get; set; }
        public string Initiator { get; set; }
        public string Place { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public Guid PeriodicityId { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }

        public virtual Periodicity Periodicity { get; set; }
    }
}
