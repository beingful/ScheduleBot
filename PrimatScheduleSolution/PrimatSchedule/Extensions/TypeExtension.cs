using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public static class TypeExtension
    {
        private static readonly Dictionary<Type, object> _defaultValues;

        static TypeExtension()
        {
            _defaultValues = new Dictionary<Type, object>
            {
                { typeof(String), String.Empty },
                { typeof(TimeSpan), TimeSpan.Zero },
                { typeof(DateTime), DateTime.MinValue }
            };
        }

        public static object GetDefaultValue(this Type type) => _defaultValues.GetValueOrDefault(type);
    }
}
