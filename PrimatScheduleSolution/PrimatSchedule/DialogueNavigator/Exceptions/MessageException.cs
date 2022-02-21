using System;

namespace PrimatScheduleBot
{
    public class MessageException : Exception
    {
        protected MessageException(string message) : base(message) { }
    }
}
