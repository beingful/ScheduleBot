using System;
using System.Reflection;
using Microsoft.Data.SqlClient;
using DataBaseExploitation.Models;
using SecuredData;

namespace DataBaseExploitation
{
    public static class Querier
    {
        public static Schedule GetQuerySelection(string date)
        {
            var schedule = new Schedule();

            using (SqlConnection connection = new SqlConnection(Data.GetConnectionString()))
            {
                string sql = $"SELECT * FROM PrimatSchedule('{date}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pair = new Lesson();

                            SetPropertyValue(reader, pair);
                            schedule.AddPair(pair);
                        }
                    }
                }
            }
            return schedule;
        }

        private static void SetPropertyValue(SqlDataReader reader, Lesson pair)
        {
            PropertyInfo[] propertyInfo;
            Type pairType = typeof(Lesson);
            propertyInfo = pairType.GetProperties();

            foreach (var property in propertyInfo)
            {
                int index = reader.GetOrdinal(property.Name);
                string value = Convert.ToString(reader.GetValue(index));

                try
                {
                    property.SetValue(pair, value);
                }
                catch
                {
                    pair.Professor.Add(value);
                }
            }
        }
    }
}
