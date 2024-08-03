using System;

namespace PrimatScheduleBot
{
    public partial class MailingList
    {
        public Guid Id { get; set; }

        public string ChatId { get; set; }

        public TimeSpan Time { get; set; }
    }
}
