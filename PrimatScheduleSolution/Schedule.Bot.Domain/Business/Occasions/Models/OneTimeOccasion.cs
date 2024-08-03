namespace Schedule.Bot.Domain.Business.Occasions.Models;

public sealed record class OneTimeOccasion(string ChatId, string Owner, string Name, TimeSpan Start, DateTime Date)
    : Occasion(ChatId, Owner, Name, Start);