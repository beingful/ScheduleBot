using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PrimatScheduleBot
{
    public class TimeAttribute : ValidationAttribute
    {
        private bool _result;

        public override bool IsValid(object value)
        {
            TimeSpan time;

            if (value is string strTime)
            {
                _result = TimeSpan.TryParse(strTime, CultureInfo.CurrentCulture, out time);
            }

            return _result;
        }
    }
}