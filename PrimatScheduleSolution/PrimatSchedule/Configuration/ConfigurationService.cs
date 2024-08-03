using Microsoft.Extensions.Configuration;

namespace PrimatScheduleBot.Configuration;

internal sealed class ConfigurationService(IConfiguration _configuration)
{
    public TSection GetSection<TSection>(string? section = null) where TSection : IConfigurationSection
    {
        return _configuration.GetSection(section ?? typeof(TSection).Name).Get<TSection>()!;
    }
}
