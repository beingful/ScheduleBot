namespace PrimatScheduleBot
{
    public class ScheduleToMessage
    {
        private readonly Schedule _schedule;

        public ScheduleToMessage(Schedule schedule) => _schedule = schedule;

        public string AllToMessage()
        {
            string result = string.Empty;
            int eventsCount = _schedule.Events.Count;

            for (int i = 0; i < eventsCount; i++)
            {
                var parser = new EventToMessage(_schedule[i]);

                result += $"{i + 1}.\t{parser.ParseAll()}";

                if (i < eventsCount - 1)
                {
                    result += "\n\n";
                }
            }

            return result;
        }

        public string ToMessage()
        {
            string result = string.Empty;
            int eventsCount = _schedule.Events.Count;

            if (eventsCount is 0) {
                throw new NoOneScheduleException();
            }

            for (int i = 0; i < eventsCount; i++)
            {
                var parser = new EventToMessage(_schedule[i]);

                result += parser.Parse();

                if (i < eventsCount - 1)
                {
                    result += "\n";
                }
            }

            return result;
        }
    }
}
