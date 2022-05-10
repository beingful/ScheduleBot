using System;

namespace PrimatScheduleBot
{
    public static class CompareExtension
    {
        public static bool GreaterThan<T>(this T? value, T? compared) where T : struct, IComparable<T>
        {
            bool result = true;

            if (value != null)
            {
                result = value.Value.CompareTo(compared.Value) is 1;
            }

            return result;
        }

        public static bool EqualsTo(this object value, object compared) 
        {
            bool result = false;

            if (value.Equals(compared))
            {
                result = true;
            }
            else if (value is string strValue && compared is string strCompared)
            {
                result = String.IsNullOrEmpty(strValue) && String.IsNullOrEmpty(strCompared);
            }

            return result;
        }
    }
}
