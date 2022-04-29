using System;

namespace PrimatScheduleBot
{
    public class EventPropertyReflectionUsage
    {
        private readonly string _propertyName;
        private readonly string _propertyValue;
        private readonly IPeriodicity _period;
        private readonly InstanceReflection<Event> _instanceReflection;

        public EventPropertyReflectionUsage(string propertyName, string propertyValue, Event @event, IPeriodicity period)
        {
            _propertyName = propertyName;
            _propertyValue = propertyValue;
            _period = period;
            _instanceReflection = new InstanceReflection<Event>(@event);
        }

        public void SetValue()
        {
            if (_propertyName is nameof(Event.Date))
            {
                SetDay();
                SetPeriodicity();
            }
            else
            {
                _instanceReflection.ConvertAndSetValue(_propertyName, _propertyValue);
            }
        }

        private void SetDay()
        {
            object value = _period.TryGetDate(_propertyValue);

            _instanceReflection.SetValue(_propertyName, value);
        }

        private void SetPeriodicity()
        {
            var periodicity = _period.GetPeriodicity();

            _instanceReflection.SetValue(nameof(Event.PeriodicityId), periodicity);
        }

        //private DateTime GetDay()
        //{
        //    var converter = new Converter<string>(_propertyValue, typeof(DateTime));

        //    return (DateTime)converter.TryGetValue();
        //}
    }
}