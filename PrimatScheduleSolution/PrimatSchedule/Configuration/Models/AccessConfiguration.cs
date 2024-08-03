namespace PrimatScheduleBot.Configuration.Models;

internal sealed record class AccessConfiguration(string Claim, string Value) : IConfigurationSection;
