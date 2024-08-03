using PrimatScheduleBot.Behavior.States;
using PrimatScheduleBot.Behavior.Transitions;
using System.Collections.Generic;
using Telegram.Bot.Types;

namespace PrimatScheduleBot.Behavior;

internal sealed class BotBehavior
{
    List<Transition> _transitions;

    public BotBehavior(Update update)
    {
        _transitions =
        [
            new Transition(From: new Root(), To: new Start(),
                If: new It<string>(update.Message!.Text!).Is(Commands.Start))
        ];
    }
}
