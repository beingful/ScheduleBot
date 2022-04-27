using System;

namespace PrimatScheduleBot
{
    public class Start : ICommand
    {
        private readonly string _token;
        private readonly UIBehaviour _uiBehaviour;
        //private IStartable _mailingList;
        //private void 

        public Start(string token/*UIBehaviour uiBehaviour*/)
        {
            _token = token;
            //_uiBehaviour = uiBehaviour;
        }

        public UI Execute(ChatInfo info)
        {
            UI ui = null; 

            //if (_uiBehaviour.IsSuchAKeyExist(info.LastMessage))
            //{
            //    ui = _uiBehaviour.GetUI(info.LastMessage);
            //}
            //else
            //{
            //    TryStart(info);

            //    ui = new UI("Ви підписалися на щоденну розсилку.");
            //}

            return ui;
        }

        private void TryStart(ChatInfo info)
        {
            //MessageValidator.ValidateMessage(_mailingList is null);
            
            //TimeSpan time = TryGetTime(info.LastMessage);
            //_mailingList = new Mailing(info.ChatId);

            //_mailingList.Start(time, _token);
        }

        //private TimeSpan TryGetTime(string message)
        //{
        //    //MessageValidator.ValidateTime(message);

        //    //return TimeSpan.Parse(message);
        //}
    }
}