namespace PrimatScheduleBot
{
    public class TimeFormatException : MessageException
    {
        private const string _message = "Не знаю, з якого ти всесвіту, але в моєму такого часу не існує.";

        public TimeFormatException() : base(new UI(_message, Stickers.Dragon)) { }
    }
}
