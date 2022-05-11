using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public partial class Periodicity
    {
        public Periodicity() => Events = new HashSet<Event>();

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
