namespace PrimatScheduleBot
{
    public class TooEarlyDateException : MessageException
    {
        private const string _message = "Навіщо дивитись в минуле? Краще подивись в майбутнє!";

        public TooEarlyDateException() : base(new UI(_message, Stickers.Laying)) { }
    }
}
