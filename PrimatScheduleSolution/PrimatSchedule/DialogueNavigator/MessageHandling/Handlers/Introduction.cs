using System;
using System.Collections.Generic;

namespace PrimatScheduleBot
{
    [Serializable]
    public sealed class Introduction : ICommand
    {
        private readonly UIBehaviour _ui;
        private readonly StateBehaviour _state;

        public Introduction(StateBehaviour state)
        {
            _state = state;

            _ui = new UIBehaviour(new Dictionary<string, UI>
            {
                { Commands.Help, new UI($"Привіт, мене звати Бізі Піггі, " +
                $"і я допоможу тобі легко і зручно керувати своїм розкладом. " +
                $"Ти зможеш додавати, видаляти і змінювати одноразові (на певну дату) чи циклічні (на день тижня) події. " +
                $"Циклічна подія відбуватиметься кожен тиждень в той день, який ти вкажеш. Але це ще не все! " +
                $"Ти також зможеш передивлятись розклад на певну дату чи день тижня. І це ще не все! " +
                $"Ти зможеш підписатися на розсилку розкладу. Що це значить? " +
                $"Кожен день рівно в зазначену тобою годину я надсилатиму тобі весь розклад на цей день! " +
                $"Правда круто?", new List<string> { Buttons.Yes })
                },
                { Buttons.Yes, new UI(null, Stickers.Love) }
            });
        }

        public UI Execute(ChatInfo info) 
        {
            //UI ui;

            //if (_ui.IsSuchAKeyExist(info.LastMessage))
            //{
            //    ui = _ui.GetUI(info.LastMessage);
            //}
            //else
            //{

            //}

            return null;
        }
    }
}
