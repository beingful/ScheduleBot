using System;

namespace PrimatScheduleBot
{
    public class EndWithoutStartException : MessageException
    {
        private const string _message = "Подія не може мати кінець, але не мати початку.";

        public EndWithoutStartException() : base(new UI(_message, Stickers.Fighting)) { }
    }
}
