using System;

namespace PrimatScheduleBot
{
    public class EndWithoutStartException : MessageException
    {
        private const string _message = "Як це так: подія має кінцевий час, але немає початкового? " +
            "А-ну негайно виправляй!";

        public EndWithoutStartException() : base(new UI(_message, Stickers.Fighting)) { }
    }
}
