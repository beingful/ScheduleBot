namespace PrimatScheduleBot
{
    public class QuartzStartException : MessageException
    {
        private const string _message = "Ти вже підписаний на щоденну розсилку розкладу. " +
            "Якщо хочеш отримувати розклад в інший час, відпишись від існуючої розписки і підпишись знаву.";

        public QuartzStartException() : base(new UI(_message, Stickers.Back)) { }
    }
}
