using Schedule.Bot.Domain.Business.Occasions.Enums;

namespace Schedule.Bot.Domain.Business.Occasions.Models;

public sealed record class DailyOccasion(string Owner, string ChatId, string Name, TimeSpan Start, DayOfWeek DayOfWeek)
    : Occasion(Owner, ChatId, Name, Start, Pereodicity.Daily);
