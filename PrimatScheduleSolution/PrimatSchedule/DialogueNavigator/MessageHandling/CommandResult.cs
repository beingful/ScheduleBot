using System.Collections.Generic;

namespace PrimatScheduleBot
{
    public class CommandResult
    {
        public readonly Dictionary<int, UI> _results;

        public CommandResult()
        {
            _results = new Dictionary<int, UI>
            {
                { -1, new UI("Я не зміг виконати операцію. Це все тому, що ти недостатньо добре знаєш мене! " +
                    "Якщо не розумієш, в чому може бути помилка, раджу тобі ще раз вислухати мою історію. /introduce", Stickers.Crying) },
                { 1, new UI("Ура, ми молодці! Операція успішна!", Stickers.Done) }
            };
        }
    }
}
