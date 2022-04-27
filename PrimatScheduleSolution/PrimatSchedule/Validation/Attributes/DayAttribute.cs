using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class DayAttribute : ValidationAttribute
    {
        private bool _result;

        public override bool IsValid(object value)
        {
            if (value is string strValue)
            {
                _result = DaysOfWeek.DoesSuchADayExist(strValue);
            }

            return _result;
        }
    }
}