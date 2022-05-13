namespace PrimatScheduleBot
{
    public class DateFormatException : MessageException
    {
        private const string _message = "Упс... Здається, ти невірно задав дату. Спробуй формат ДД.ММ.РРРР!";

        public DateFormatException() : base(new UI(_message, Stickers.Fail)) { }
    }
}
