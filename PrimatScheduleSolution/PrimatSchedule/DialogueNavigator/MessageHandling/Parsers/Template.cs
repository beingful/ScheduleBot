using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PrimatScheduleBot
{
    public class Template
    {
        public readonly Dictionary<string, string> _parser;

        public Template()
        {
            _parser = new Dictionary<string, string>();

            SetParser();
        }


        public void SetParser()
        {
            Type pairType = typeof(Event);
            PropertyInfo[] propertyInfo = pairType.GetProperties();

            foreach (var property in propertyInfo)
            {
                var displayAttribute = GetAttribute<DisplayAttribute>(property);

                if (displayAttribute != null)
                {
                    _parser.Add(displayAttribute.Name, property.Name);
                }
            }
        }

        private T GetAttribute<T>(PropertyInfo property) where T : Attribute
        {
            Attribute attribute = property.GetCustomAttribute(typeof(T), false);
            return (T)attribute;
        }

        public string ToMessageAllExcluded(string excludedProperty = null)
        {
            string result = string.Empty;

            foreach (var unit in _parser)
            {
                if (unit.Value != excludedProperty)
                {
                    result += $"{unit.Key}:\n";
                }
            }

            return result;
        }
    }
}
