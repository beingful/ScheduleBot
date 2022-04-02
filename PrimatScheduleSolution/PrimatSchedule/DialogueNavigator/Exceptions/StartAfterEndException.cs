namespace PrimatScheduleBot
{
    public class StartAfterEndException : MessageException
    {
        private const string _message = "Ахахаха, кінець події має бути пізніше ніж початок, ти так не вважаєш?";

        public StartAfterEndException() : base(new UI(_message, Stickers.What)) { }
    }
}
