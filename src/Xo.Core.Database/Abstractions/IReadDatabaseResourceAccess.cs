namespace Xo.Core.Database.Abstractions;

public interface IReadDatabaseResourceAccess
{
    //Task<TPaged> GetPagedAsync<TQuery, TPaged, TQueryResult>(
    //TQuery qry,
    //uint pageSize,
    //uint pageNumber
    //)
    //where TQuery : IQuery
    //where TPaged : IPagedRows<TQueryResult>
    //where TQueryResult : IQueryResult;

    //Task<TQueryResult> GetAsync<TQuery, TQueryResult>(TQuery qry)
    //where TQuery : IQuery
    //where TQueryResult : IQueryResult;

    //Task<TQueryResult> GetByKeyAsync<TKey, TQueryResult>(TKey key)
    //where TKey : struct
    //where TQueryResult : IQueryResult;

    Task<IEnumerable<TQueryResult>> QueryAsync<TQuery, TQueryResult>(TQuery qry)
        where TQuery : IBaseQuery
        where TQueryResult : IQueryResult;

    Task<TQueryResult> QueryFirstOrDefaultAsync<TQuery, TQueryResult>(TQuery qry)
        where TQuery : IBaseQuery
        where TQueryResult : IQueryResult;
}
