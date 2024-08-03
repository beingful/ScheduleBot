using Schedule.Bot.Domain.Database.Connection;
using Schedule.Bot.Domain.Database.Exceptions;
using Schedule.Bot.Firestore.Connection.Parsing;
using Schedule.Bot.Firestore.Exceptions;

namespace Schedule.Bot.Firestore.Connection;

public sealed class FirestoreAccessProvider : DbAccessProvider<FirestoreAccess>
{
    private readonly (char Parameter, char KeyValue) _separators = (';', '=');
    private readonly List<string> _accessParameters = [];
    private readonly ConnectionStringDissector _connectionStringDissector;

    public FirestoreAccessProvider(string connectionString) : base(connectionString)
    {
        Array.ForEach(typeof(FirestoreAccess).GetProperties(), (property) =>
        {
            _accessParameters.Add(property.Name);
        });

        _connectionStringDissector = new ConnectionStringDissector(connectionString,
            parameterSeparator: ';', keyValueSeparator: '=');
    }

    public override FirestoreAccess Provide()
    {
        Dictionary<string, string> connectionParameters = _connectionStringDissector.GetConnectionParameters();

        ValidateAccessParameters(connectionParameters);

        return CreateAccess(connectionParameters);
    }

    private void ValidateAccessParameters(Dictionary<string, string> connectionParameters)
    {
        if (_accessParameters.FirstOrDefault(x => !connectionParameters.ContainsKey(x)) is string missedParameter)
        {
            throw new MissingConnectionStringParameterException(missedParameter);
        }

        if (connectionParameters.Keys.FirstOrDefault(x => !_accessParameters.Contains(x)) is string unsupportedParameter)
        {
            throw new UnsupportedConectionStringParameterException(unsupportedParameter);
        }
    }

    private FirestoreAccess CreateAccess(Dictionary<string, string> parameters)
    {
        Type firestoreAccessType = typeof(FirestoreAccess);

        object firestoreAccess = Activator.CreateInstance(firestoreAccessType)!;

        foreach (KeyValuePair<string, string> parameter in parameters)
        {
            firestoreAccessType
                .GetProperty(parameter.Key)!
                .SetValue(firestoreAccess, parameter.Value);
        }

        return (FirestoreAccess)firestoreAccess;
    }
}
