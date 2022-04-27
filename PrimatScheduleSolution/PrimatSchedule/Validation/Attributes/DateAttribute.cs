using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class DateAttribute : ValidationAttribute
    {
        private bool _result;

        public override bool IsValid(object value)
        {
            DateTime date;

            if (value is string strDate && DateTime.TryParse(strDate, out date))
            {
                _result = true;
            }

            return _result;
        }
    }
}