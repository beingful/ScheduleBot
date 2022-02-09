using System.Collections.Generic;

namespace PrimatScheduleBot
{
    sealed class Start : Command
    {
        private readonly string _botId;
        private readonly long _chatId;

        public Start(string botId, long chatId) :
            base(new Dictionary<MessageResult, string>
            {
                { MessageResult.OK, "Ви підписалися на щоденну розсилку розкладу. Очікуйте на повідомлення о 8:30 ранку."},
                { MessageResult.NOTOK, "Ви вже підписані на щоденну розсилку розкладу."}
            })
        {
            _botId = botId;
            _chatId = chatId;
        }

        public override string DoTaskAndGetMessage()
        {
            if (PostScheduler.Start(_botId, _chatId).Result)
            {
                return Messages.GetValueOrDefault(MessageResult.OK);
            }

            return Messages.GetValueOrDefault(MessageResult.NOTOK);
        }
    }
}
