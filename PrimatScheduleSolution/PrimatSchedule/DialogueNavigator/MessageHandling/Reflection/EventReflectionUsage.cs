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

        private EventPropertyReflectionUsage GetPropertyReflectionUsage(string propertyName, string propertyValue)
            => new EventPropertyReflectionUsage(propertyName, propertyValue, _event, _period);

        public void SetValue(string propertyName, string propertyValue)
        {
            EventPropertyReflectionUsage propertyReflectionUsage = GetPropertyReflectionUsage(propertyName, propertyValue);

            propertyReflectionUsage.SetValue();
        }
    }
}
