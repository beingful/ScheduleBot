using System.Collections.Generic;

namespace PrimatScheduleBot
{
    sealed class Stop : Command
    {
        private readonly long _chatId;

        public Stop(long chatId) :
            base(new Dictionary<MessageResult, string>
            {
                { MessageResult.OK, "Ви відписалися від щоденної розсилки розкладу."},
                { MessageResult.NOTOK, "Ви ще не підписані на щоденну розсилку розкладу."}
            })
            => _chatId = chatId;

        public override string DoTaskAndGetMessage()
        {
            if (PostScheduler.Stop(_chatId).Result)
            {
                return Messages.GetValueOrDefault(MessageResult.OK);
            }

            return Messages.GetValueOrDefault(MessageResult.NOTOK);
         }
    }
}
