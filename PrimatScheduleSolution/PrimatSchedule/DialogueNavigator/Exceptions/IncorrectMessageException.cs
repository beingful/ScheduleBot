namespace PrimatScheduleBot
{
    public class IncorrectMessageException : MessageException
    {
        private const string _message = "Я не розумію, що ти від мене хочеш, скористайся довідкою /help, щоб ми змогли порозумітися.";
        public IncorrectMessageException() : base(_message) { }
    }
}
