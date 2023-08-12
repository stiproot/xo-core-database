namespace Xo.Core.Database.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseUtilityServices(
        this IServiceCollection @this,
        IConfiguration configuration
    )
    {
        @this.TryAddSingleton<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
        @this.TryAddSingleton<IDapperWrapper, DapperWrapper>();
        @this.ConfigureOptions<DatabaseOptions>(configuration, nameof(DatabaseOptions));
        return @this;
    }
}
