namespace Schedule.Bot.Domain.Database.Connection;

public abstract class DbConnector<TAccess, TDatabase> where TDatabase : class
{
    protected readonly DbAccessProvider<TAccess> AccessProvider;

    public DbConnector(DbAccessProvider<TAccess> accessProvider)
    {
        AccessProvider = accessProvider;
    }

    public abstract TDatabase Connect();
}
