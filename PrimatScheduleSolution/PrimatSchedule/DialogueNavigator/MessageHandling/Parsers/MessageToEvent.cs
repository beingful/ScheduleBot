using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PrimatScheduleBot
{
    public class MessageToEvent
    {
        private const char _separator = ':';
        private readonly string _message;
        private readonly Event _event;

        public MessageToEvent(string message, Event @event)
        {
            _message = message;
            _event = @event;
        }

        public Event Parse()
        {
            Dictionary<string, string> lines = GetKeyValuePairs();

            SetEventProperties(lines);
            SetGuidIfItsNew();
            _event.Validate();

            return _event;
        }

        public void SetGuidIfItsNew()
        {
            if (_event.Id == Guid.Empty)
            {
                _event.Id = Guid.NewGuid();
            }
        }

        private Dictionary<string, string> GetKeyValuePairs()
        {
            return _message
                .Split('\n')
                .ToDictionary(line => GetSubstring(line), line => GetSubstring(line, true).Trim('\t', '\r', ' '));
        }

        private string GetSubstring(string line, bool toTheEnd = false)
        {
            int separatorIndex = line.IndexOf(_separator);
            string substring;

            try
            {
                if (!toTheEnd)
                {
                    substring = line.Substring(0, separatorIndex);
                }
                else
                {
                    substring = line.Substring(separatorIndex + 1);
                }
            }
            catch
            {
                throw new IncorrectMessageException();
            }

            return substring;
        }

        private void SetEventProperties(Dictionary<string, string> lines)
        {
            var template = new Template();
            Type pairType = typeof(Event);

            foreach (var line in lines)
            {
                string propertyName;

                try
                {
                    propertyName = template._parser[line.Key];
                }
                catch
                {
                    throw new IncorrectMessageException();
                }

                PropertyInfo property = pairType.GetProperty(propertyName);

                if (line.Value != string.Empty)
                {
                    property.SetValue(_event, line.Value);
                }
            }
        }
    }
}
