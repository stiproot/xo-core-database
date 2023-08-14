namespace Xo.Core.Database;

public class DapperWrapper : IDapperWrapper
{
    public DapperWrapper() { }

    public async Task<TQueryResult> QueryFirstOrDefaultAsync<TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.StoredProcedure,
        IDbTransaction? transaction = null
    )
        => await connection.QueryFirstOrDefaultAsync<TQueryResult>(sql, parameters, commandType: commandType, transaction: transaction);

    public async Task<IEnumerable<TQueryResult>> QueryAsync<TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.StoredProcedure,
        IDbTransaction? transaction = null
    )
        => await connection.QueryAsync<TQueryResult>(sql, parameters, commandType: commandType, transaction: transaction);

    public async Task<TPaged> PagedQueryAsync<TPaged, TQueryResult>(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.Text,
        IDbTransaction? transaction = null
    )
        where TQueryResult : IQueryResult
        where TPaged : IPagedRows<TQueryResult>
    {
        var results = await connection.QueryMultipleAsync(sql, parameters, commandType: commandType, transaction: transaction);

        var pageable = await results.ReadFirstAsync<TPaged>();

        pageable.Rows = await results.ReadAsync<TQueryResult>();

        return pageable;
    }

    public async Task ExecuteCommandAsync(
        IDbConnection connection,
        string sql,
        DynamicParameters parameters,
        CommandType commandType = CommandType.Text,
        IDbTransaction? transaction = null
    )
        => await connection.ExecuteAsync(sql, parameters, commandType: commandType, transaction: transaction);
}
