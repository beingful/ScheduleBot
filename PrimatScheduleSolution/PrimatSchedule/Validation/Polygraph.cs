namespace PrimatScheduleBot
{
    public class Polygraph<ExceptionType> : IValidator 
        where ExceptionType : MessageException, new()
    {
        private readonly bool _statement;

        public Polygraph(bool statement) => _statement = statement;

        public void Validate()
        {
            if (!_statement)
            {
                throw new ExceptionType();
            }
        }
    }
}