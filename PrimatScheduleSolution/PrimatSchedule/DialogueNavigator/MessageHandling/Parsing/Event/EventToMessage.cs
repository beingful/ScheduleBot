using System;

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

            return $"{timeValue.Hours.ToString("D2")}:{timeValue.Minutes.ToString("D2")}";
        }

        private string GetProperty(string property) 
        {
            string result = property;

            if (!String.IsNullOrEmpty(property))
            {
                result += "\n";
            }

            return result;
        }

        public string Parse() => GetName() + GetBaseEventInformation();

        public string ParseAll<T>() where T : IPeriodicity, new ()
        {
            var result = String.Empty;

            var properties = new PropertiesDisplay<T>();
            var reflection = new EventReflectionUsage<T>(_event);

            foreach (var props in properties.DisplayValues)
            {
                object value = reflection.GetValue(props.Key);

                result += GetFullProperty(props.Value, value);
            }

            return result;
        }

        private string GetFullProperty(string displayName, object value) => $"{displayName}: {value}\n";
    }
}