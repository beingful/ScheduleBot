using System;

namespace PrimatScheduleBot
{
    public static class Validation
    {
        private static void Validate(IValidator validator) => validator.Validate();

        public static void CorrectMessage(bool isCorrect)
            => Validate(new Polygraph<IncorrectMessageException>(isCorrect));

        public static void EndAfterStart(TimeSpan? startTime, TimeSpan? endTime)
            => Validate(new Polygraph<EndBeforeStartException>(endTime.GreaterThan(startTime)));

        public static void StartAndEndAreNormal(TimeSpan? startTime, TimeSpan? endTime)
            => Validate(new Polygraph<EndWithoutStartException>(!(startTime is null && endTime != null)));

        public static void DateIsCorrect(DateTime date)
            => Validate(new Polygraph<TooEarlyDateException>(date >= DateTime.Today));

        public static void ValidateDoesEventExist(bool isCorrect)
            => Validate(new Polygraph<EventIsDeletedException>(isCorrect));

        public static void ValidateQuartzStop(bool isCorrect)
            => Validate(new Polygraph<QuartzStopException>(isCorrect));

        public static void ScheduleNotEmpty(int eventCount)
            => Validate(new Polygraph<NoOneScheduleException>(eventCount > 0));

        public static void RequiredIsNotDefault(object instance)
            => Validate(new FullValidationMode<IsDefaultAttribute, NullValueException>(instance));

        public static void TimeIsValid(string time)
            => Validate(new FullValidationMode<TimeAttribute, TimeFormatException>(time));

        public static void DateIsValid(string date)
            => Validate(new FullValidationMode<DateAttribute, DateFormatException>(date));

        public static void ValidateDay(string day)
            => Validate(new FullValidationMode<DayAttribute, DayFormatException>(day));

        public static void ValidateTimeForDuplications(Event @event)
            => Validate(new FullValidationMode<EventTimeAttribute, EventDateTimeDuplicationException>(@event));

        public static void ValidateEvent(Event @event)
            => Validate(new AttributeAccordance<EventAttribute>(@event));

        public static void Equal<T>(T value, T compared) => CorrectMessage(value.EqualsTo(compared));

        public static void NotEqual<T>(T value, T compared) => CorrectMessage(!value.EqualsTo(compared));

        public static void NotNull(object instance) => CorrectMessage(instance != null);

        public static void NullValue(object instance) => Equal(instance, null);
    }
}