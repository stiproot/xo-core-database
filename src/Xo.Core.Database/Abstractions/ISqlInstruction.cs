namespace Xo.Core.Database.Abstractions;

public interface ISqlInstruction
{
    string Sql { get; init; }
    DynamicParameters Parameters { get; init; }
    CommandType CommandType { get; init; }
}
