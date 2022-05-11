using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class StateBehaviourEntryPoint
    {
        public static MemorableStateBehaviour Initialize(string token)
        {
            return new MemorableStateBehaviour(new Dictionary<string, ICommand>
            {
                { Commands.Start, new Command(new UIBehaviour(new Dictionary<string, UI>
                    {
                        { Commands.Start, new UI($"Привіт, я твій Щоденник! " +
                        $"Якщо ти ще не знайом з усіма моїми перевагами, почитай мою біографію /introduce. " +
                        $"\nБеремо уявну ручку і розпочинаємо!", Stickers.Typing,
                            new List<string> { Buttons.Start, Buttons.Stop, Buttons.Insert, Buttons.Edit }) }
                    }), new StateBehaviour(new Dictionary<string, ICommand>
                    {
                        { Buttons.Start, new Start(token, new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Start, new UI("О котрій годині ви хочете отримувати сповіщення?") }
                            })) 
                        },
                        { Buttons.Stop, new Stop(new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Stop, new UI("Ви відписалися від щоденної розсилки.") }
                            })) 
                        },
                        { Buttons.Insert, new Command(new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Insert, new UI("Я можу додати подію в розклад на певну дату чи на день тижня, що обираєш?",
                                    new List<string> { Buttons.Date, Buttons.Day }) },
                            }), new StateBehaviour(new Dictionary<string, ICommand>
                            {
                                { Buttons.Date, new Insert<Date>() },
                                { Buttons.Day, new Insert<Day>() }
                            }))
                        },
                        { Buttons.Edit, new Command(new UIBehaviour(new Dictionary<string, UI>
                            {
                                { Buttons.Edit, new UI("Я можу знайти твій росклад по даті або по дню тижня, обери свяй шлях...",
                                new List<string> { Buttons.Date, Buttons.Day }) }
                            }), new StateBehaviour(new Dictionary<string, ICommand>
                            {
                                { Buttons.Date, new Calendar<Date>(new UIBehaviour(new Dictionary<string, UI>
                                    {
                                        { Buttons.Date, new UI("Введи дату, щоб я зміг знайти твій розклад.") }
                                    }))
                                },
                                { Buttons.Day, new Calendar<Day>(new UIBehaviour(new Dictionary<string, UI>
                                    {
                                        { Buttons.Day, new UI("Введи день тижня, щоб я зміг знайти твій розклад.") }
                                    })) 
                                }
                            }))
                        }
                    }))
                },
                { Commands.Help, new Introduce() }
            });
        }
    }
}