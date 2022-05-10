namespace PrimatScheduleBot
{
    public class EndBeforeStartException : MessageException
    {
        private const string _message = "Кінець події має бути пізніше ніж початок.";

        public EndBeforeStartException() : base(new UI(_message, Stickers.What)) { }
    }
}
