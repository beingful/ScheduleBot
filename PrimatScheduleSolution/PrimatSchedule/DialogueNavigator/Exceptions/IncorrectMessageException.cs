namespace PrimatScheduleBot
{
    public class IncorrectMessageException : MessageException
    {
        public IncorrectMessageException() : base(new UI("Р-а-а-р-р-р", Stickers.Dragon)) { }
    }
}
