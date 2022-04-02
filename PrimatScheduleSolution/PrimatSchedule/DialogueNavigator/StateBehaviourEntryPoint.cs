using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class StateBehaviourEntryPoint
    {
        public StateBehaviour Initialize(string token)
        {
            return new StateBehaviour(new Dictionary<string, ICommand>
            {
                { Commands.Start, new Command(new UIBehaviour(new Dictionary<string, UI>
                    {
                        { Commands.Start, new UI($"Привіт, я твій Щоденник! " +
                        $"Якщо ти ще не знайом з усіма моїми перевагами, почитай мою біографію /introduce. " +
                        $"\nБеремо уявну ручку і розпочинаємо!", Stickers.Typing,
                            new List<string> { Buttons.Start, Buttons.Stop, Buttons.Insert, Buttons.Edit }) }
                    }), new StateBehaviour(new Dictionary<string, ICommand>
                    {
                        { Buttons.Start, new Start(token) },
                        { Buttons.Stop, new Stop() },
                        { Buttons.Insert, new Command(new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Insert, new UI("Я можу додати подію в розклад на певну дату чи на день тижня, що обираєш?",
                                    new List<string> { Buttons.Date, Buttons.Day }) },
                            }), new StateBehaviour(new Dictionary<string, ICommand>
                            {
                                { Buttons.Date, new Insert(Querier.InsertEventOnDate) },
                                { Buttons.Day, new Insert(Querier.InsertEventOnDay) }
                            }))
                        },
                        { Buttons.Edit, new Command(new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Edit, new UI("Я можу знайти твій росклад по даті або по дню тижня, обери свяй шлях...",
                                new List<string> { Buttons.Date, Buttons.Day }) }
                            }), new StateBehaviour(new Dictionary<string, ICommand>
                            {
                                { Buttons.Date, new OnDate() },
                                { Buttons.Day, new OnDay() }
                            }))
                        }
                    }))
                },
                { Commands.Help, new Introduce() }
            });
        }
    }
}
