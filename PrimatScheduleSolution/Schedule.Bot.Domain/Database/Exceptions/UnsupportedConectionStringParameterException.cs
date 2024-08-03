namespace Schedule.Bot.Firestore.Exceptions;

public sealed class UnsupportedConectionStringParameterException : ArgumentException
{
    private const string _message = "Provided connection string containes unsupported parameter.";

    public UnsupportedConectionStringParameterException(string parameterName) : base(_message, parameterName)
    {
    }
}
