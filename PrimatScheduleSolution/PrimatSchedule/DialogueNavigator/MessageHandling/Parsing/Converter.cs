using System;
using System.ComponentModel;

namespace PrimatScheduleBot
{
    public class Converter<T>
    {
        private readonly T _from;
        private readonly TypeConverter _converter;

        public Converter(T from, Type to)
        {
            _from = from;
            _converter = GetTypeConverter(to);
        }

        private TypeConverter GetTypeConverter(Type type)
        {
            object to = type.GetDefaultValue();

            return TypeDescriptor.GetConverter(to);
        }

        public object TryGetValue()
        {
            MessageValidator.ValidateMessage(_converter.IsValid(_from));

            return _converter.ConvertFrom(_from);
        }
    }
}