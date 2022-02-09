using System;


namespace PrimatScheduleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            (new Navigator(Data.GetToken())).StartChating();

            Console.ReadKey();
        }
    }
}
