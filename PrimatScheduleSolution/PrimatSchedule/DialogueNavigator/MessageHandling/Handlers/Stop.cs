using System.Collections.Generic;

namespace PrimatScheduleBot
{
    sealed class Stop : Command
    {
        private readonly long _chatId;

        public Stop(long chatId) : base(new Dictionary<MessageResult, string>
            {
                { MessageResult.ALLOWED, "Ви відписалися від щоденної розсилки розкладу."},
                { MessageResult.DENIED, "Ви ще не підписані на щоденну розсилку розкладу."}
            })
        {
            _chatId = chatId;
        }

        public override string HandleAndSendAnswer()
        {
            bool canStop = PostScheduler.TryStop(_chatId).Result;
            MessageResult result = canStop ? MessageResult.ALLOWED : MessageResult.DENIED;

            return Messages.GetValueOrDefault(result);
         }
    }
}
