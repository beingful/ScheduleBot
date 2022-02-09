using System;
using DialogueNavigator;
using SecuredData;


namespace StartProject
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
