namespace Xo.Core.Database.ResourceAccess;

public class WriteDatabaseResourceAccess : IWriteDatabaseResourceAccess
{
    protected readonly IDatabaseConnectionFactory _DatabaseConnectionFactory;
    protected readonly IDapperWrapper _DapperWrapper;
    protected readonly ISqlCommandMapper _SqlCommandMapper;

    public WriteDatabaseResourceAccess(
        IDatabaseConnectionFactory databaseConnectionFactory,
        IDapperWrapper dapperWrapper,
        ISqlCommandMapper sqlCommandMapper
    )
    {
        this._DatabaseConnectionFactory = databaseConnectionFactory ?? throw new ArgumentNullException(nameof(databaseConnectionFactory));
        this._DapperWrapper = dapperWrapper ?? throw new ArgumentNullException(nameof(dapperWrapper));
        this._SqlCommandMapper = sqlCommandMapper ?? throw new ArgumentNullException(nameof(sqlCommandMapper));
    }

    public async Task ExecuteAsync(
        ICommand cmd,
        CancellationToken cancellationToken,
        IDbConnection? connection = null,
        IDbTransaction? transaction = null
    )
    {
        if (cmd.Result == null)
        {
            cmd.Result = new BaseCommandResult();
        }

        var instruction = this._SqlCommandMapper.Map(cmd);
        if (connection is null)
        {
            using var _connection = this._DatabaseConnectionFactory.Create();
            cmd.Result.Id = await this.ExecuteAsync(cmd, cancellationToken, _connection, instruction, transaction);
        }
        else
        {
            cmd.Result.Id = await this.ExecuteAsync(cmd, cancellationToken, connection, instruction, transaction);
        }
    }

    private async Task<long> ExecuteAsync(
        ICommand cmd,
        CancellationToken cancellationToken,
        IDbConnection connection,
        ISqlInstruction instruction,
        IDbTransaction? transaction
    )
    {
        return await this._DapperWrapper.QueryFirstOrDefaultAsync<long>(connection, instruction.Sql, instruction.Parameters, transaction: transaction);
    }
}
