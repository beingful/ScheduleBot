using System;
using System.Reflection;

namespace PrimatScheduleBot
{
    public class PropertyReflection<T> where T : class
    {
        private readonly T _instance;
        private readonly PropertyInfo _property;

        public PropertyReflection(T instance, string propertyName) 
        {
            _instance = instance;
            _property = GetProperty(propertyName);
        }

        private PropertyInfo GetProperty(string propertyName)
        {
            var pairType = _instance.GetType();

            return pairType.GetProperty(propertyName);
        }

        private object ConvertValue(string from)
        {
            Type type = GetExactType();
            object value = GetTypeDefaultValue();

            var converter = new Converter<string>(from, type, value);

            return converter.TryGetValue();
        }

        private Type GetExactType()
        {
            TypeDetermination type = GetTypeDetermination();

            return type.GetExactType();
        }

        private object GetTypeDefaultValue()
        {
            TypeDetermination type = GetTypeDetermination();

            return type.GetTypeDefaultValue();
        }

        private TypeDetermination GetTypeDetermination() => new TypeDetermination(_property.PropertyType);

        public void ConvertAndSetValue(string value)
        {
            object convertedValue = ConvertValue(value);

            SetValue(convertedValue);
        }

        public void SetValue(object value) => _property.SetValue(_instance, value);

        public object GetValue() => _property.GetValue(_instance);
    }
}