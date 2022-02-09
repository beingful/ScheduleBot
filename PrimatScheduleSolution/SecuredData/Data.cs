using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SecuredData
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

        public static string GetToken() => _dataJson.Value<string>("token");

        public static string GetConnectionString() => _dataJson.Value<string>("connectionString");
    }
}
