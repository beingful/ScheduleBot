using System;
using System.Collections.Generic;
using System.Linq;
namespace PrimatScheduleBot
{
    public class NullValueException : MessageException
    {
        private const string _message = "Заповни обов'язкове поле \"{0}\", а я поки почекаю.";

        public NullValueException(string memberName) : 
            base(new UI(string.Format(_message, memberName), Stickers.Fail)) {}
    }
}
