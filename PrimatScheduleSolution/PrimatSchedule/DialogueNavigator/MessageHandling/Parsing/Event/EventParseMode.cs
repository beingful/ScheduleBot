using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class EventParseMode<T> where T : IPeriodicity, new()
    {
        private const char _separator = ':';
        private readonly string _message;
        private readonly PropertiesDisplay<T> _display;

        public EventParseMode(string message, PropertiesDisplay<T> display) 
        {
            _message = message;
            _display = display;
        }

        public Dictionary<string, string> Parse()
        {
            string[] linesForParsing = _message.Split('\n');

            Dictionary<string, string> parsedProperties = GetParsedProperties(linesForParsing);

            CheckIsValid(parsedProperties.Keys);

            return parsedProperties;
        }

        private void CheckIsValid(IEnumerable<string> parsedPropertiesKeys)
        {
            string[] keys = parsedPropertiesKeys.ToArray();

            Validation.CorrectMessage(_display.AllRecognized(keys));
        }

        public Dictionary<string, string> GetParsedProperties(string[] linesForParsing)
        {
            return linesForParsing
                .ToDictionary(key =>
                {
                    int separatorIndex = GetSeparatorIndex(key);

                    return GetValue(key, 0, separatorIndex);
                }, value =>
                {
                    int separatorIndex = GetSeparatorIndex(value);

                    return GetValue(value, separatorIndex + 1, value.Length - separatorIndex - 1);
                });
        }

        private string GetValue(string line, int from, int to)
        {
            string value = line.Substring(from, to);

            value = value.Trim('\t', '\r', ' ');

            return value;
        }

        private int GetSeparatorIndex(string line)
        {
            int separatorIndex = line.IndexOf(_separator);

            Validation.NotEqual(separatorIndex, -1);

            return separatorIndex;
        }
    }
}