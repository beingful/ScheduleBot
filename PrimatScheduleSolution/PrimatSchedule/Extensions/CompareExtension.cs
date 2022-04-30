using System;

namespace PrimatScheduleBot
{
    public static class CompareExtension
    {
        public static bool IsEqual<T>(this T obj, T compared) where T : IEquatable<T>
        {
            bool result = true;

            if (obj != null)
            {
                result = obj.Equals(compared);
            }

            return result;
        }
    }
}
