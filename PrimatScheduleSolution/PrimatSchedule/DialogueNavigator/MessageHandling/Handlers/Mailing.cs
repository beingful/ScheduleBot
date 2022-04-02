using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class Mailing : ICommand
    {
        private readonly string _token;
        private readonly UIBehaviour _uiBehaviour;

        [Time]
        public string MailTime { get; private set; }

        public Mailing(string message, string token) 
        {
            _token = token;
            _uiBehaviour = new UIBehaviour(new Dictionary<string, UI>
            {
                { message, new UI($"Ви підписалися на щоденну розсилку.") }
            });
        }

        public UI Execute(ChatInfo info)
        {
            UI ui;

            MailTime = info.LastMessage;

            try
            {
                ui = _uiBehaviour.StateMachine[info.LastMessage];
            }
            catch
            {
                throw new IncorrectMessageException();
            }

            Validate();

            StartMailingList(info);

            return ui;
        }

        private void StartMailingList(ChatInfo info)
        {
            PostScheduler.Start(_token, info.ChatId, TimeSpan.Parse(MailTime));
        }

        private void Validate()
        {
            var context = new ValidationContext(this);

            Validator.ValidateObject(this, context, true);
        }
    }
}
