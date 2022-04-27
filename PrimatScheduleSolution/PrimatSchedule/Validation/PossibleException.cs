namespace PrimatScheduleBot
{
    public static class PossibleException
    {
        public static void Validate(bool isCorrect, MessageException exception)
        {
            if (!isCorrect)
            {
                throw exception;
            }
        }
    }
}
