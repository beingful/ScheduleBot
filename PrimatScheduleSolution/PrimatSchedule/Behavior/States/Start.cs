using System;

namespace PrimatScheduleBot.Behavior.States;

public class Start : ICommand
{
    public UI Execute(ChatInfo info)
    {
        UI ui;

        if (_uiBehaviour.IsSuchAKeyExist(info.LastMessage))
        {
            ui = _uiBehaviour.GetUI(info.LastMessage);
        }
        else
        {
            TryStart(info);

            ui = new UI("Ви підписалися на щоденну розсилку.");
        }

        return ui;
    }

    private void TryStart(ChatInfo info)
    {
        TimeSpan time = TryGetTime(info.LastMessage);

        _mailingList = new Mailing(info.ChatId);
        _mailingList.Start(time, _token);
    }

    private TimeSpan TryGetTime(string message)
    {
        Validation.TimeIsValid(message);

        return TimeSpan.Parse(message);
    }
}
