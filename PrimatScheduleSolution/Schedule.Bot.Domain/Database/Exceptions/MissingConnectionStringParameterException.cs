namespace Schedule.Bot.Domain.Database.Exceptions;

public class MissingConnectionStringParameterException : MissingMemberException
{
    private const string _message = "Some of connection string required parameters is missing.";

    public MissingConnectionStringParameterException() : base(_message)
    {
    }

    public MissingConnectionStringParameterException(string parameterName) : base(_message, parameterName)
    {
    }
}
