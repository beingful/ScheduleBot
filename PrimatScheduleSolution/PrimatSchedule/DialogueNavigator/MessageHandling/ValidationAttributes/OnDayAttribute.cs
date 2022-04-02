using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace PrimatScheduleBot
{
    public class OnDayAttribute : ValidationAttribute
    {
        private readonly string[] _days;

        public OnDayAttribute()
        {
            _days = CultureInfo.GetCultureInfo("uk-UA").DateTimeFormat.DayNames;
        }
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string weekDay = (value as string)?.ToLower();

                return _days.Any(day => day == weekDay);
            }

            return true;
        }
    }
}
