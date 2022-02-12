using System;


namespace PrimatScheduleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Navigator navigator = new Navigator(Data.Token);

            navigator.StartChating();

            Console.ReadKey();
        }
    }
}
