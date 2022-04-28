using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PrimatScheduleBot
{
    public static class RefTypeCopyingExtension
    {
        public static T DeepClone<T>(this T obj) where T : class
        {
            using var ms = new MemoryStream();

            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);

            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }
}
