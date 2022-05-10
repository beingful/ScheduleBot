namespace PrimatScheduleBot
{
    public class TooEarlyDateException : MessageException
    {
        private const string _message = "Цей день вже минув.";

        public TooEarlyDateException() : base(new UI(_message, Stickers.Laying)) { }
    }
}
