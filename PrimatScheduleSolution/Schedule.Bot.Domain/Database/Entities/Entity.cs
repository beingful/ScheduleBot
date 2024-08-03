namespace Schedule.Bot.Domain.Database.Entities;

public class Entity<TIdentifier> where TIdentifier : struct
{
    public required TIdentifier Id { get; init; }
}
