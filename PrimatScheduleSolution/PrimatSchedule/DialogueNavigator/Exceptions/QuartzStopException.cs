namespace PrimatScheduleBot
{
    public class QuartzStopException : MessageException
    {
        private const string _message = "Притормози, ти ще не підписаний на щоденну розсилку розкладу. " +
            "Якщо хочеш відписатися, спершу підпишись.";

        public QuartzStopException() : base(new UI(_message, Stickers.Back)) { }
    }
}
