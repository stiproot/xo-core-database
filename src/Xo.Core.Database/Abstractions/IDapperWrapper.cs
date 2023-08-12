namespace Xo.Core.Database.Abstractions;

public interface IDapperWrapper
{
    Task<TQueryResult> QueryFirstOrDefaultAsync<TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.StoredProcedure,
        IDbTransaction? transaction = null
    );

    Task<IEnumerable<TQueryResult>> QueryAsync<TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.StoredProcedure,
        IDbTransaction? transaction = null
    );

    Task<TPaged> PagedQueryAsync<TPaged, TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.Text,
        IDbTransaction? transaction = null
    )
        where TQueryResult : IQueryResult
        where TPaged : IPagedRows<TQueryResult>;

    Task ExecuteCommandAsync(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.Text,
        IDbTransaction? transaction = null
    );
}
