namespace PrimatScheduleBot
{
    public class NoDateAndDayException : MessageException
    {
        private const string _message = "Дата чи день має бути вказана обов'язково, інакше це подія на \"ніколи\".";

        public NoDateAndDayException() : base(new UI(_message, Stickers.Back)) { }
    }
}
