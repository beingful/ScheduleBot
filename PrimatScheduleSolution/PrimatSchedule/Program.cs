using System;

namespace PrimatScheduleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot(Data.Token);

            bot.StartChating();

            Console.ReadKey();
        }
    }
}
