using System.IO;
using Newtonsoft.Json.Linq;

namespace PrimatScheduleBot
{
    public static class Data
    {
        private static JObject _dataJson;

        static Data()
        {
            using (var file = new StreamReader(@"D:\Json\ScheduleProjectConsts.json"))
            {
                _dataJson = JObject.Parse(file.ReadToEnd());
            }
        }

        public static string Token => _dataJson.Value<string>("token");

        public static string ConnectionString => _dataJson.Value<string>("connectionString");
    }
}
