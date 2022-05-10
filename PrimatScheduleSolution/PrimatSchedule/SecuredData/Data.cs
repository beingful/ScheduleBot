using System.Configuration;

namespace PrimatScheduleBot
{
    public static class Data
    {
        public static string Token => ConfigurationManager.AppSettings["BotToken"];

        public static string ConnectionString => 
            ConfigurationManager.ConnectionStrings["Timetable"].ConnectionString;
    }
}