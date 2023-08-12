namespace Xo.Core.Database.ResourceAccess;

public class ReadDatabaseResourceAccess : IReadDatabaseResourceAccess
{
    protected readonly IDatabaseConnectionFactory _DatabaseConnectionFactory;
    protected readonly IDapperWrapper _DapperWrapper;
    protected readonly ISqlQueryMapper _SqlQueryMapper;

    public ReadDatabaseResourceAccess(
        IDatabaseConnectionFactory databaseConnectionFactory,
        IDapperWrapper dapperWrapper,
        ISqlQueryMapper sqlQueryMapper
    )
    {
        this._DatabaseConnectionFactory = databaseConnectionFactory ?? throw new ArgumentNullException(nameof(databaseConnectionFactory));
        this._DapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        this._SqlQueryMapper = sqlQueryMapper ?? throw new ArgumentNullException(nameof(sqlQueryMapper));
    }

    //public async Task<TPaged> GetPagedAsync<TQuery, TPaged, TQueryResult>(
    //TQuery qry,
    //uint pageSize,
    //uint pageNumber
    //)
    //where TQuery : IQuery
    //where TPaged : IPagedRows<TQueryResult>
    //where TQueryResult : IQueryResult
    //{
    //var instruction = this._SqlQueryMapper.Map(qry);
    //using var connection = this._DatabaseConnectionFactory.Create();

    //var results = await this._DapperWrapper.PagedQueryAsync<TPaged, TQueryResult>(connection, instruction.Sql, instruction.Parameters);

    //return results;
    //}

    //public async Task<TQueryResult> GetAsync<TQuery, TQueryResult>(TQuery qry)
    //where TQuery : IQuery
    //where TQueryResult : IQueryResult
    //{
    //var instruction = this._SqlQueryMapper.Map(qry);
    //using var connection = this._DatabaseConnectionFactory.Create();

    //var result = await this._DapperWrapper.QueryFirstOrDefaultAsync<TQueryResult>(connection, instruction.Sql, instruction.Parameters);

    //return result;
    //}

    //public async Task<TQueryResult> GetByKeyAsync<TKey, TQueryResult>(TKey key)
    //where TKey : struct
    //where TQueryResult : IQueryResult
    //{
    //var instruction = this._SqlQueryMapper.Map<TKey>(key);
    //using var connection = this._DatabaseConnectionFactory.Create();

    //var result = await this._DapperWrapper.QueryFirstOrDefaultAsync<TQueryResult>(connection, instruction.Sql, instruction.Parameters);

    //return result;
    //}

    public async Task<IEnumerable<TQueryResult>> QueryAsync<TQuery, TQueryResult>(TQuery qry)
        where TQuery : IBaseQuery
        where TQueryResult : IQueryResult
    {
        var instruction = this._SqlQueryMapper.Map(qry);
        using var connection = this._DatabaseConnectionFactory.Create();

        var result = await this._DapperWrapper.QueryAsync<TQueryResult>(connection, instruction.Sql, instruction.Parameters);

        return result;
    }

    public async Task<TQueryResult> QueryFirstOrDefaultAsync<TQuery, TQueryResult>(TQuery qry)
        where TQuery : IBaseQuery
        where TQueryResult : IQueryResult
    {
        var instruction = this._SqlQueryMapper.Map(qry);
        using var connection = this._DatabaseConnectionFactory.Create();

        var result = await this._DapperWrapper.QueryFirstOrDefaultAsync<TQueryResult>(connection, instruction.Sql, instruction.Parameters, commandType: CommandType.StoredProcedure);

        return result;
    }
}
