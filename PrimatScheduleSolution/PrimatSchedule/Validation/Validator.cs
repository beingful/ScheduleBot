namespace PrimatScheduleBot
{
    public static class Validator
    {
        public static void CheckDateTimeDuplications(bool isCorrect)
            => PossibleException.Validate(isCorrect, new EventDateTimeDuplicationException());
    }
}
