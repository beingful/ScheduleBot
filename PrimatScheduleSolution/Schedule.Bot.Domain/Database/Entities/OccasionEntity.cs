using Schedule.Bot.Domain.Business.Occasions.Enums;

namespace Schedule.Bot.Domain.Database.Entities;

public sealed class OccasionEntity : Entity<int>
{
    public required string Owner { get; init; }

    public required string ChatId { get; init; }

    public required string Name { get; init; }

    public required Pereodicity Pereodicity { get; init; }

    public required TimeSpan Start { get; init; }

    public TimeSpan? End { get; init; }

    public DateTime Date { get; init; }

    public DayOfWeek DayOfWeek { get; init; }

    public string? Place { get; init; }

    public string? Link { get; init; }

    public string? Description { get; init; }

    public IEnumerable<string>? Participants { get; init; }
}
