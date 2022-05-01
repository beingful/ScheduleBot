namespace PrimatScheduleBot
{
    public class EventReflectionUsage
    {
        private readonly Event _event;
        private readonly IPeriodicity _period;

        public EventReflectionUsage(Event @event, IPeriodicity period)
        {
            _event = @event;
            _period = period;
        }

        private EventPropertyReflectionUsage GetPropertyReflectionUsage(string propertyName)
            => new EventPropertyReflectionUsage(propertyName, _event, _period);

        public void SetValue(string propertyName, string propertyValue)
        {
            EventPropertyReflectionUsage propertyReflectionUsage = GetPropertyReflectionUsage(propertyName);

            propertyReflectionUsage.SetValue(propertyValue);
        }

        public object GetValue(string propertyName)
        {
            EventPropertyReflectionUsage propertyReflectionUsage = GetPropertyReflectionUsage(propertyName);

            return propertyReflectionUsage.GetValue(propertyName);
        }
    }
}
