using System;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class OnDateBoundsAttribute : ValidationAttribute
    {
        private bool _result;

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                if (value is DateTime date)
                {
                    _result = date >= DateTime.Today;
                }
            }
            else
            {
                _result = true;
            }

            return _result;
        }
    }
}
