using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class ScheduleToMessage
    {
        private readonly List<Event> _schedule;

        public ScheduleToMessage(IEnumerable<Event> schedule) => _schedule = schedule.ToList();

        public string Convert(bool toOrderedList = false)
        {
            MessageValidator.ValidateIsScheduleEmpty(_schedule.Count);

            string result = string.Empty;

            if (toOrderedList)
            {
                OrderedList(result);
            }
            else
            {
                UnorderedList(result);
            }

            return result;
        }

        public void OrderedList(string result)
        {
            for (int i = 0; i < _schedule.Count; i++)
            {
                result += $"{i + 1}.\t{ParseEvent(_schedule[i])}";
            }
        }

        private string ParseEvent(Event @event)
        {
            var parser = new EventToMessage(@event);

            return $"{parser.Parse()}\n\n";
        }

        public void UnorderedList(string result)
        {
            foreach (var @event in _schedule)
            {
                result += $"{ParseEvent(@event)}\n\n";
            }
        }
    }
}