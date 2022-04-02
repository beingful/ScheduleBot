using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class TimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value is string strTime)
                {
                    TimeSpan time;

                    if (strTime != string.Empty && !TimeSpan.TryParse(strTime, out time))
                    {
                        throw new TimeFormatException();
                    }

                    return true;
                }

                return false;
            }

            return true;
        }
    }
}
