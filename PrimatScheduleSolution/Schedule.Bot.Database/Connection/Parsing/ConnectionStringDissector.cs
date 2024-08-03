namespace Schedule.Bot.Firestore.Connection.Parsing;

internal sealed class ConnectionStringDissector
{
    private readonly string _connectionString;
    private readonly (char Parameter, char KeyValue) _separators;

    public ConnectionStringDissector(string connectionString, char parameterSeparator, char keyValueSeparator)
    {
        _connectionString = connectionString;
        _separators = (parameterSeparator, keyValueSeparator);
    }

    public Dictionary<string, string> GetConnectionParameters()
    {
        string[] connectionStringSectors = _connectionString.Split(_separators.Parameter);

        Dictionary<string, string> connectionParameters = new(connectionStringSectors.Length);

        foreach (string parameter in connectionStringSectors)
        {
            string[] nameValue = parameter.Split(_separators.KeyValue);

            connectionParameters.Add(nameValue[0], nameValue[1]);
        }

        return connectionParameters;
    }
}
