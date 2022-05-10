namespace PrimatScheduleBot
{
    public class EventReflectionUsage<T> where T : IPeriodicity, new()
    {
        private readonly Event _event;

        public EventReflectionUsage(Event @event) => _event = @event;

        private EventPropertyReflectionUsage<T> GetPropertyReflectionUsage(string propertyName) 
            => new EventPropertyReflectionUsage<T>(propertyName, _event);

        public void SetValue(string propertyName, string propertyValue)
        {
            EventPropertyReflectionUsage<T> propertyReflectionUsage = GetPropertyReflectionUsage(propertyName);

            propertyReflectionUsage.SetValue(propertyValue);
        }

        public object GetValue(string propertyName)
        {
            EventPropertyReflectionUsage<T> propertyReflectionUsage = GetPropertyReflectionUsage(propertyName);

            return propertyReflectionUsage.GetValue(propertyName);
        }
    }
}
