namespace Xo.Core.Database.Factories;

public class DatabaseConnectionFactory : IDatabaseConnectionFactory
{
    private readonly DatabaseOptions _options;

    public DatabaseConnectionFactory(IOptions<DatabaseOptions> options)
    {
        this._options = options.Value ?? throw new ArgumentNullException(nameof(options));
        if (string.IsNullOrEmpty(this._options.ConnectionString)) throw new ArgumentException("Connection string is not set...");
    }

    public IDbConnection Create()
        => new NpgsqlConnection(this._options.ConnectionString);
}
