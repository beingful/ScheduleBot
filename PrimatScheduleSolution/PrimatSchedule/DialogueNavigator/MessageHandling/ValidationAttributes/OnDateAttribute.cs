using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PrimatScheduleBot
{
    public class OnDateAttribute : ValidationAttribute
    {
        private const string _format = "yyyy-MM-dd";
        private readonly CultureInfo _culture = CultureInfo.CurrentCulture;
        private readonly DateTimeStyles _style = DateTimeStyles.None;
        private bool _result;

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value is string strDate && !string.IsNullOrEmpty(strDate))
                {
                    DateTime date;

                    if (!DateTime.TryParseExact(strDate, _format, _culture, _style, out date))
                    {
                        return false;
                    }

                    _result = true;
                }
            }

            _result = true;

            return _result;
        }
    }
}