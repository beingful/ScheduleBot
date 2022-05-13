namespace PrimatScheduleBot
{
    public class TooLateException : MessageException
    {
        private const string _message = "Вже пізно планувати щось на цей час.";

        public TooLateException() : base(new UI(_message, Stickers.Waiting)) { }
    }
}
