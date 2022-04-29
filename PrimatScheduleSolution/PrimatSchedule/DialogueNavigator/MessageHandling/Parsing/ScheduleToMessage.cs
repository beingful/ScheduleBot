using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class ScheduleToMessage
    {
        private readonly IEnumerable<Event> _schedule;

        public ScheduleToMessage(IEnumerable<Event> schedule) => _schedule = schedule;

        public string Convert(bool toOrderedList = false)
        {
            try
            {
                _schedule.Count();
            }
            catch
            {
                MessageValidator.ValidateIsScheduleEmpty(_schedule.Count());
            }
            MessageValidator.ValidateIsScheduleEmpty(_schedule.Count());

            string result = string.Empty;

            if (toOrderedList)
            {
                result = OrderedList(result);
            }
            else
            {
                result = UnorderedList(result);
            }

            return result;
        }

        public string OrderedList(string result)
        {
            int counter = 1;

            foreach(var @event in _schedule)
            {
                result += $"{counter}.\t{ParseEvent(@event)}";

                counter++;
            }

            return result;
        }

        private string ParseEvent(Event @event)
        {
            var parser = new EventToMessage(@event);

            return $"{parser.Parse()}\n\n";
        }

        public string UnorderedList(string result)
        {
            foreach (var @event in _schedule)
            {
                result += $"{ParseEvent(@event)}\n\n";
            }

            return result;
        }
    }
}