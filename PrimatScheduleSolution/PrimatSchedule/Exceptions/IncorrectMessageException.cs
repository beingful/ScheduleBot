namespace PrimatScheduleBot
{
    public class IncorrectMessageException : MessageException
    {
        public IncorrectMessageException() : base(new UI("")) { }
    }
}
