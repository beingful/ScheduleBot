namespace Schedule.Bot.Domain.Database.Connection;

public abstract class DbAccessProvider<TAccessConfiguration>
{
    protected readonly string ConnectionString;

    public DbAccessProvider(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public abstract TAccessConfiguration Provide();
}
