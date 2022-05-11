namespace PrimatScheduleBot
{
    public class TimeFormatException : MessageException
    {
        private const string _message = "Неправильно вказан час. Спробуй 24-годинний формат.";

        public TimeFormatException() : base(new UI(_message, Stickers.Dragon)) { }
    }
}
