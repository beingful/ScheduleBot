namespace PrimatScheduleBot
{
    public class DateFormatException : MessageException
    {
        private const string _message = "Упс... Здається, ти невірно задав дату. Спробуй формат РРРР-ММ-ДД.";

        public DateFormatException() : base(new UI(_message, Stickers.Fail)) { }
    }
}
