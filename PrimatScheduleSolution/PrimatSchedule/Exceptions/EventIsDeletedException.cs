namespace PrimatScheduleBot
{
    public class EventIsDeletedException : MessageException
    {
        private const string _message = "Ця подія була видалена із розкладу.";

        public EventIsDeletedException() : base(new UI(_message, Stickers.Fail)) { }
    }
}
