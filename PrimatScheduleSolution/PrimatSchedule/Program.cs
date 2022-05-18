using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PrimatScheduleBot
{
    class Program
    {
        static async Task Main()
        {
            var builder = new HostBuilder();

            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddAzureStorageBlobs();
            });

            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();

                string instrumentationKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];

                if (!string.IsNullOrEmpty(instrumentationKey))
                {
                    b.AddApplicationInsightsWebJobs(o => o.InstrumentationKey = instrumentationKey);
                }
            });

            builder.UseEnvironment(EnvironmentName.Development);

            using var host = builder.Build();

            var bot = new Bot(Data.Token);

            bot.StartChating();

            await host.RunAsync();
        }
    }
}
