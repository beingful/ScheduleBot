namespace PrimatScheduleBot
{
    public class NullValueException : MessageException
    {
        private const string _message = "Переконайся, що заповнив всі обов'язкові поля: Назва, Дата/День.";

        public NullValueException() : base(new UI(_message, Stickers.Fail)) {}
    }
}
