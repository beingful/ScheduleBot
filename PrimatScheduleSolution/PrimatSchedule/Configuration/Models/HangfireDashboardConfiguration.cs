namespace PrimatScheduleBot.Configuration.Models;

internal sealed record class HangfireDashboardConfiguration(string Title, AccessConfiguration Access) : IConfigurationSection;
