namespace PrimatScheduleBot
{
    public class QuartzStartException : MessageException
    {
        private const string _message = "Ви вже підписані на щоденну розсилку розкладу.";
        public QuartzStartException() : base(_message) { }
    }
}
