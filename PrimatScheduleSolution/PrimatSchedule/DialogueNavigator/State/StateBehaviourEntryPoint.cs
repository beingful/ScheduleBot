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
                        { Commands.Start, new UI($"Привіт, я Бізі Піггі! " +
                        $"\nЯкщо ти ще не знайом зі мною, швидше тицяй на /introduce " +
                        $"і нумо за роботу!",
                        new List<string> { Buttons.Start, Buttons.Stop, Buttons.Insert, Buttons.Edit }, 1, Stickers.Typing) }
                    }), Transition(token))
                },
                { Commands.Help, new Command(new UIBehaviour(new Dictionary<string, UI>
                    {
                        { Commands.Help, new UI($"Привіт, мене звати Бізі Піггі, " +
                        $"і я допоможу тобі легко і зручно керувати своїм розкладом. " +
                        $"Ти зможеш додавати, видаляти і змінювати одноразові (на певну дату) чи циклічні (на день тижня) події. " +
                        $"Циклічна подія відбуватиметься кожен тиждень в той день, який ти вкажеш. Але це ще не все! " +
                        $"Ти також зможеш передивлятись розклад на певну дату чи день тижня. І це ще не все! " +
                        $"Ти зможеш підписатися на розсилку розкладу. Що це значить? " +
                        $"Кожен день рівно в зазначену тобою годину я надсилатиму тобі весь розклад на цей день! " +
                        $"Правда круто?", new List<string> { Buttons.Yes }, 1)
                        },
                        { Buttons.Yes, new UI(null, Stickers.Love) }
                    }), Transition(token) ) 
                }
            });
        }

        private static StateBehaviour Transition(string token)
        {
            return new StateBehaviour(new Dictionary<string, ICommand>
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
                        { Buttons.Insert, new UI("Додати подію в розклад на дату чи на день тижня?",
                        new List<string> { Buttons.Date, Buttons.Day }, 2) },
                    }), new StateBehaviour(new Dictionary<string, ICommand>
                    {
                        { Buttons.Date, new Insert<Date>() },
                        { Buttons.Day, new Insert<Day>() }
                    }))
                },
                { Buttons.Edit, new Command(new UIBehaviour(new Dictionary<string, UI>
                    {
                        { Buttons.Edit, new UI("Знайти твій розклад по даті чи по дню тижня?",
                        new List<string> { Buttons.Date, Buttons.Day }, 2) }
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
            });
        }
    }
}