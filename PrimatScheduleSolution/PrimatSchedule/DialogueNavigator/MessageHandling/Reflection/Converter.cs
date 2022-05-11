using System;
using System.ComponentModel;

namespace PrimatScheduleBot
{
    public class Converter<T>
    {
        private readonly T _from;
        private readonly TypeConverter _converter;
        private readonly object _typeDefaultValue;

        public Converter(T from, Type to, object typeDefaultValue)
        {
            _from = from;
            _typeDefaultValue = typeDefaultValue;
            _converter = GetTypeConverter(to);
        }

        private TypeConverter GetTypeConverter(Type type) 
            => TypeDescriptor.GetConverter(_typeDefaultValue);

        public object TryGetValue()
        {
            object value = null;

            if (_converter.IsValid(_from))
            {
                value = _converter.ConvertFrom(_from);
            }
            else
            {
                Validation.CorrectMessage(_from.Equals(String.Empty));
            }

            return value;
        }
    }
}