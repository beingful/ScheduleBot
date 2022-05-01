namespace PrimatScheduleBot
{
    public class EventPropertyReflectionUsage
    {
        private readonly string _propertyName;
        private readonly IPeriodicity _period;
        private readonly InstanceReflection<Event> _instanceReflection;

        public EventPropertyReflectionUsage(string propertyName, Event @event, IPeriodicity period)
        {
            _propertyName = propertyName;
            _period = period;
            _instanceReflection = new InstanceReflection<Event>(@event);
        }

        public void SetValue(string value)
        {
            if (_propertyName is nameof(Event.Date))
            {
                SetDay(value);
                SetPeriodicity();
            }
            else
            {
                _instanceReflection.ConvertAndSetValue(_propertyName, value);
            }
        }

        private void SetDay(string value)
        {
            object date = _period.TryGetDate(value);

            _instanceReflection.SetValue(_propertyName, value);
        }

        private void SetPeriodicity()
        {
            var periodicity = _period.GetPeriodicity();

            _instanceReflection.SetValue(nameof(Event.PeriodicityId), periodicity);
        }

        public object GetValue(string propertyName) => _instanceReflection.GetValue(propertyName);
    }
}