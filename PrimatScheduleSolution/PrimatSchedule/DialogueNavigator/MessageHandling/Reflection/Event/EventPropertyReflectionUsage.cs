using System;

namespace PrimatScheduleBot
{
    public class EventPropertyReflectionUsage<T> where T : IPeriodicity, new()
    {
        private readonly string _propertyName;
        private readonly IPeriodicity _period;
        private readonly InstanceReflection<Event> _instanceReflection;

        public EventPropertyReflectionUsage(string propertyName, Event @event)
        {
            _propertyName = propertyName;
            _period = new T();
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
            object date = _period.CalculateDate(value);

            _instanceReflection.SetValue(_propertyName, date);
        }

        private void SetPeriodicity()
        {
            var periodicity = _period.GetPeriodicity();

            _instanceReflection.SetValue(nameof(Event.PeriodicityId), periodicity);
        }

        public object GetValue(string propertyName) 
        {
            object value = _instanceReflection.GetValue(propertyName);

            LeadValueToCorrectView(ref value);

            return value;
        }

        private void LeadValueToCorrectView(ref object value)
        {
            if (value is DateTime date)
            {
                value = date.ToString("dd.MM.yyyy");
            }
            else if (value is TimeSpan time)
            {
                value = time.ToString(@"hh\:mm");
            }
        }
    }
}