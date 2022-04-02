namespace PrimatScheduleBot
{
    public class DayAttribute : OnDayAttribute
    {
        public override bool IsValid(object value)
        {
            if (!base.IsValid(value))
            {
                throw new DayFormatException();
            }

            return true;
        }
    }
}
