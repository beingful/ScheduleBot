namespace Schedule.Bot.Domain.Database.Connection;

public class DbStringAccessProvider : DbAccessProvider<string>
{
    public DbStringAccessProvider(string connectionString) : base(connectionString)
    {
    }

    public override string Provide()
    {
        return ConnectionString;
    }

    public override string ToString()
    {
        return ConnectionString;
    }
}
