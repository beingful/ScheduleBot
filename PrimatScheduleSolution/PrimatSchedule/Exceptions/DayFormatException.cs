namespace PrimatScheduleBot
{
    public class DayFormatException : MessageException
    {
        private const string _message = "Мені здається, такого дня тижня не існує.";

        public DayFormatException() : base(new UI(_message, Stickers.Fail)) { }
    }
}
