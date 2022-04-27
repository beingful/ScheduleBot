﻿using System;

namespace PrimatScheduleBot
{
    public class EventToMessage
    {
        private readonly Event _event;

        public EventToMessage(Event @event) => _event = @event;

        private string GetBaseEventInformation() 
            => GetProperty(_event.Initiator) + GetProperty(_event.Place) + GetTime() + GetProperty(_event.Description); 

        private string GetName()
        {
            string result = _event.Link is null ? Name() : NameWithLink();

            return result + "\n";
        }

        private string Name() => $"{_event.Name}";

        private string NameWithLink() => $"[{Name()}]({_event.Link})";

        private string GetTime()
        {
            var result = string.Empty;
            TimeSpan? startTime = _event.StartTime;

            if (startTime.HasValue)
            {
                result += Time(startTime);

                TimeSpan? endTime = _event.EndTime;

                if (endTime.HasValue)
                {
                    result += $" - {Time(endTime)}";
                }

                result += "\n";
            }

            return result;
        }

        private string Time(TimeSpan? time)
        {
            TimeSpan timeValue = time.Value;

            var timeLine = $"{timeValue.Hours}:{timeValue.Minutes}";

            return timeLine;
        }

        private string GetProperty(object property) => property is null ? String.Empty : $"{property}\n";

        public string Parse() => GetName() + GetBaseEventInformation();

        public string ParseAll(IPeriodicity period)
            => Name() + period.GetProperty(_event.Date) + GetBaseEventInformation() + GetProperty(_event.Link);
    }
}