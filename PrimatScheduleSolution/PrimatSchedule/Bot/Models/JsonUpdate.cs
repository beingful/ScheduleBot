using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PrimatScheduleBot.ScheduleBot.Models;

internal sealed class JsonUpdate : Update
{
    public static async ValueTask<JsonUpdate?> BindAsync(HttpContext context)
    {
        using var streamReader = new StreamReader(context.Request.Body);

        var updateJsonString = await streamReader.ReadToEndAsync();

        return JsonConvert.DeserializeObject<JsonUpdate>(updateJsonString);
    }
}
