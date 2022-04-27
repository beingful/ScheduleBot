using System;
using System.Reflection;

namespace PrimatScheduleBot
{
    public class PropertyReflection<T> where T : class, new()
    {
        private readonly T _instance;
        private readonly PropertyInfo _property;

        private PropertyReflection(T instance) => _instance = instance;

        public PropertyReflection(T instance, string propertyName) : this(instance) 
            => _property = GetProperty(propertyName);

        private PropertyInfo GetProperty(string propertyName)
        {
            var pairType = _instance.GetType();

            return pairType.GetProperty(propertyName);
        }

        private object ConvertValue(string from)
        {
            Type exactType = GetExactType();

            var converter = new Converter<string>(from, exactType);

            return converter.TryGetValue();
        }

        private Type GetExactType()
        {
            Type type = _property.PropertyType;

            Type underlyingType = Nullable.GetUnderlyingType(type);

            return underlyingType ?? type;
        }

        public void ConvertAndSetValue(object value)
        {
            object convertedValue = ConvertValue(value as string);

            SetValue(convertedValue);
        }

        public void SetValue(object value) => _property.SetValue(_instance, value);
    }
}