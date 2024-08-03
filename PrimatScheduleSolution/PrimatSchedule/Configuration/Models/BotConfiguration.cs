namespace PrimatScheduleBot.Configuration.Models;

internal sealed record class BotConfiguration(string Token, string UserName, string HostAddress) : IConfigurationSection
{
    public WebhookConfiguration Webhook { get; } = new(
        Name: nameof(PrimatScheduleBot),
        Route: $"/bot/{Token.Replace(':', '_')}",
        Url: $"{HostAddress}/bot/{Token.Replace(':', '_')}");
}
