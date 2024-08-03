using Schedule.Bot.Domain.Business.Occasions.Enums;

namespace Schedule.Bot.Domain.Business.Occasions.Models;

public abstract record class Occasion(string Owner, string ChatId, string Name, TimeSpan Start, Pereodicity Pereodicity = Pereodicity.None)
{
    public string? Place { get; init; }

    public TimeSpan? End { get; init; }

    public string? Link { get; init; }

    public string? Description { get; init; }

    public IEnumerable<string>? Participants { get; init; }
}
