using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class MessageToEvent<T> where T : IPeriodicity, new()
    {
        private readonly Event _event;
        private readonly EventParseMode<T> _parseMode;
        private readonly PropertiesDisplay<T> _display;
        private readonly EventReflectionUsage<T> _reflectionUsage;

        private MessageToEvent(string message)
        {
            _display = new PropertiesDisplay<T>();
            _parseMode = new EventParseMode<T>(message, _display);
        }

        public MessageToEvent(string message, Event @event) : this(message)
        {
            _event = @event;
            _reflectionUsage = new EventReflectionUsage<T>(@event);
        }

        public MessageToEvent(string chatId, string message) : 
            this(message, new Event { Id = Guid.NewGuid(), ChatId = chatId })
        { }

        public Event Convert()
        {
            Dictionary<string, string> parsedProperties = _parseMode.Parse();

            foreach (var parsedProperty in parsedProperties)
            {
                string propertyName = _display.GetKey(parsedProperty.Key);

                _reflectionUsage.SetValue(propertyName, parsedProperty.Value);
            }

            Validation.ValidateEvent(_event);

            return _event;
        }
    }
}