namespace Xo.Core.Database.Abstractions;

public interface IWriteDatabaseResourceAccess
{
    Task ExecuteAsync(
        ICommand cmd,
        CancellationToken cancellationToken,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null
    );
}
