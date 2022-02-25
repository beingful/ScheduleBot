using System;


namespace PrimatScheduleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot(Data.Token);

            bot.StartChating();

            Console.ReadKey();
        }
    }
}
