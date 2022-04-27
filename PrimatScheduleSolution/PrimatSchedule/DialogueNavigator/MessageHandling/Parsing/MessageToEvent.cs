﻿using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class MessageToEvent
    {
        private readonly Event _event;
        private readonly EventParseMode _parseMode;
        private readonly PropertiesDisplay _display;
        private readonly EventReflectionUsage _reflectionUsage;

        private MessageToEvent(string message, IPeriodicity period)
        {
            _parseMode = new EventParseMode(message, period);
            _display = new PropertiesDisplay(period);
            _reflectionUsage = new EventReflectionUsage(_event, period);
        }

        public MessageToEvent(string message, IPeriodicity period, Event @event) : this(message, period)
            => _event = @event;

        public MessageToEvent(string chatId, string message, IPeriodicity period) : this(message, period)
        {
            _event = new Event 
            { 
                Id = new Guid(), 
                ChatId = chatId 
            };
        }

        public Event Convert()
        {
            Dictionary<string, string> parsedProperties = _parseMode.Parse();

            foreach (var parsedProperty in parsedProperties)
            {
                string propertyName = _display.GetValue(parsedProperty.Key);

                _reflectionUsage.SetValue(propertyName, parsedProperty.Value);
            }

            MessageValidator.ValidateEvent(_event);

            return _event;
        }
    }
}