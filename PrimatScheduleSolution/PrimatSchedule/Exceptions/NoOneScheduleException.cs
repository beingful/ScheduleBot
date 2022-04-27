namespace PrimatScheduleBot
{
    public class NoOneScheduleException : MessageException
    {
        private const string _message = "Розслабон... Твій розклад на цей день вільний.";

        public NoOneScheduleException() : base(new UI(_message, Stickers.Sleeping)) { }
    }
}
