using System.Collections.Generic;
using System.Linq;

namespace PrimatScheduleBot
{
    public class PropertiesDisplay
    {
        public readonly Dictionary<string, string> DisplayValues;

        public PropertiesDisplay(IPeriodicity period)
        {
            DisplayValues = new Dictionary<string, string>
            {
                { nameof(Event.Name), "Назва" },
                { nameof(Event.Initiator), "Ініціатор" },
                { nameof(Event.Place), "Місце проведення" },
                { nameof(Event.Date), period.Name },
                { nameof(Event.StartTime), "Час початку" },
                { nameof(Event.EndTime), "Час завершення" },
                { nameof(Event.Link), "Посилання" },
                { nameof(Event.Description), "Опис" }
            };
        }

        public bool IsSuchAKeyExist(string key) => DisplayValues.ContainsKey(key);

        public string GetValue(string key) => DisplayValues[key];

        public bool AllRecognized(string[] keys) => keys.All(key => DisplayValues.ContainsKey(key));

        public override string ToString()
        {
            string result = string.Empty;

            foreach (var display in DisplayValues)
            {
                result += $"\n{display.Key}:";
            }

            return result;
        }
    }
}
