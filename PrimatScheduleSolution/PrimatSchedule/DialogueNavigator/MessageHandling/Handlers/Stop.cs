namespace PrimatScheduleBot
{
    public sealed class Stop : ICommand
    {
        private readonly UIBehaviour _uiBehaviour;

        public Stop(UIBehaviour uiBehaviour) => _uiBehaviour = uiBehaviour;

        public UI Execute(ChatInfo info)
        {
            Validate(info.LastMessage);
            TryStop(info.ChatId);

            return _uiBehaviour.GetUI(info.LastMessage);
        }

        private void TryStop(string chatId)
        {
            var mailing = new Mailing(chatId);

            mailing.Stop();
        }

        private void Validate(string message)
        {
            bool doesKeyExist = _uiBehaviour.IsSuchAKeyExist(message);

            MessageValidator.ValidateMessage(doesKeyExist);
        }
    }
}