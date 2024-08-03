using Schedule.Bot.Domain.Business.Occasions.Enums;

namespace Schedule.Bot.Domain.Business.Occasions.Models;

public sealed record class MonthlyOccasion(string Owner, string ChatId, string Name, TimeSpan Start, DateTime Date)
    : Occasion(Owner, ChatId, Name, Start, Pereodicity.Mothly);