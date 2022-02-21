using System;


namespace PrimatScheduleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot navigator = new Bot(Data.Token);

            navigator.StartChating();

            Console.ReadKey();
        }
    }
}
