using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class DateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var dateFormatAttribute = new OnDateAttribute();

            if (!dateFormatAttribute.IsValid(value))
            {
                throw new DateFormatException();
            }

            var dateBoundsAttribute = new OnDateBoundsAttribute();
            var date = DateTime.Parse(value as string);

            if (!dateBoundsAttribute.IsValid(date))
            {
                throw new TooEarlyDateException();
            }

            return true;
        }
    }
}
