using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrimatScheduleBot
{
    public class MessageValidator
    {
        private static bool IsValid(object instance, ValidationAttribute attribute)
        {
            var context = new ValidationContext(instance);
            var validationAttributes = new List<ValidationAttribute> { attribute };

            return Validator.TryValidateValue(instance, context, null, validationAttributes);
        }

        private static void Validate(bool isCorrect, MessageException exception)
        {
            if (!isCorrect)
            {
                throw exception;
            }
        }

        private static void Validate(object message, ValidationAttribute attribute, MessageException exception)
        {
            bool condition = IsValid(message, attribute);

            Validate(condition, exception);
        }

        public static void ValidateIsScheduleEmpty(int eventsCount) => Validate(eventsCount > 0, new NoOneScheduleException());

        public static void ValidateIsEndBeforeStart(TimeSpan? startTime, TimeSpan? endTime) 
            => Validate(!(endTime <= startTime), new EndBeforeStartException());

        public static void ValidateIsEndWithoutStart(TimeSpan? startTime, TimeSpan? endTime)
            => Validate(!(startTime is null && endTime != null), new EndWithoutStartException());

        public static void ValidateRequiredIsDefault<T>(T instance, T defaultValue) where T : IEquatable<T>
            => Validate(!instance.IsEqual(defaultValue), new NullValueException());

        public static void ValidateIsDateCorrect(DateTime date) => Validate(date >= DateTime.Today, new TooEarlyDateException());

        public static void ValidateMessage(bool isCorrect) => Validate(isCorrect, new IncorrectMessageException());

        public static void ValidateTime(string message) => Validate(message, new TimeAttribute(), new TimeFormatException());

        public static void ValidateDate(string message) => Validate(message, new DateAttribute(), new DateFormatException());

        public static void ValidateDay(string message) => Validate(message, new DayAttribute(), new DayFormatException());

        public static void ValidateEvent(Event @event) => IsValid(@event, new EventValidationAttribute());

        public static void ValidateQuartzStop(bool isCorrect) => Validate(isCorrect, new QuartzStopException());

        public static void ValidateTimeForDuplications(Event @event) 
            => Validate(@event, new EventTimeAttribute(), new EventDateTimeDuplicationException());

        public static void ValidateDoesEventExist(bool isCorrect) => Validate(isCorrect, new EventIsDeletedException());
    }
}