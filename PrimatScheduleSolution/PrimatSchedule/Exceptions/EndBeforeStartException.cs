namespace PrimatScheduleBot
{
    public class EndBeforeStartException : MessageException
    {
        private const string _message = "Ахахаха, кінець події має бути пізніше ніж початок, ти так не вважаєш?";

        public EndBeforeStartException() : base(new UI(_message, Stickers.What)) { }
    }
}
