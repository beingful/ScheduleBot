namespace PrimatScheduleBot
{
    public class DayFormatException : MessageException
    {
        private const string _message = "Воу-воу, полгеше, такого дня тижня не існує.";

        public DayFormatException() : base(new UI(_message, Stickers.Fail)) { }
    }
}
