public class ApplicationPostgresContext
{
    private readonly string _connectionString;

    public ApplicationPostgresContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetConnectionString() => _connectionString;
}
