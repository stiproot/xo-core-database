namespace Xo.Core.Database.Abstractions;

public interface IDatabaseConnectionFactory
{
    IDbConnection Create();
}
