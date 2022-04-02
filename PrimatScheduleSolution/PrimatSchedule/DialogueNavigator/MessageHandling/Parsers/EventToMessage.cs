using System;
using System.Globalization;

namespace PrimatScheduleBot
{
    public class EventToMessage
    {
        private readonly Event _event;

        public EventToMessage(Event @event) => _event = @event;

        public string ParseAll()
        {
            string result = GetName() 
                + GetProperty(_event.Initiator) 
                + GetProperty(_event.Place)
                + GetTime() 
                + GetProperty(_event.Date) 
                + GetProperty(_event.Day)
                + GetProperty(_event.Description);

            return result;
        }

        public string Parse()
        {
            string result = GetName()
                + GetProperty(_event.Initiator)
                + GetProperty(_event.Place)
                + GetTime()
                + GetProperty(_event.Description);

            return result;
        }

        private string GetName()
        {
            string result;

            if (_event.Link is null)
            {
                result = $"{_event.Name}\n";
            }
            else
            {
                result = $"[{_event.Name}]({_event.Link})\n";
            }

            return result;
        }

        private string GetTime()
        {
            string result;

            try
            {
                var startTime = TimeSpan.Parse(_event.StartTime);

                result = $"{startTime.Hours}:{startTime.Minutes}";

                try
                {
                    var endTime = TimeSpan.Parse(_event.EndTime);

                    result += $"-{endTime.Hours}:{endTime.Minutes}\n";
                }
                catch
                {
                    result += "\n";
                }
            }
            catch
            {
                result = String.Empty;
            }

            return result;
        }

        private string GetProperty(string property) 
        { 
            return property is null ? String.Empty : $"{property}\n";
        }
    }
}
